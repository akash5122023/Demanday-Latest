using AdvanceCRM.Administration;
using AdvanceCRM.Common;
using AdvanceCRM.Common.Repositories;
using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Serenity.Abstractions;

namespace AdvanceCRM.Common
{
    public class TimelineBehavior : BaseSaveDeleteBehavior, IImplicitBehavior, IRetrieveBehavior, IFieldBehavior
    {
        public Field Target { get; set; }
        private readonly IRequestContext _context;
        private readonly ISqlConnections _connections;

        public TimelineBehavior(IRequestContext context, ISqlConnections connections)
        {
            _context = context;
            _connections = connections;
        }

        public bool ActivateFor(IRow row)
        {
            if (ReferenceEquals(null, Target))
                return false;

            var attr = Target.GetAttribute<TimelineEditorAttribute>();
            if (attr == null)
                return false;

            if (Target.ValueType != typeof(List<TimelineRow>))
            {
                throw new ArgumentException(String.Format("Field '{0}' in row type '{1}' has a TimelineEditorAttribute " +
                    "but its property type is not a List<TimelineRow>!",
                    Target.PropertyName ?? Target.Name, row.GetType().FullName));
            }

            return true;
        }

        public void OnAfterExecuteQuery(IRetrieveRequestHandler handler) { }
        public void OnBeforeExecuteQuery(IRetrieveRequestHandler handler) { }
        public void OnPrepareQuery(IRetrieveRequestHandler handler, SqlQuery query) { }
        public void OnValidateRequest(IRetrieveRequestHandler handler) { }

        public void OnReturn(IRetrieveRequestHandler handler)
        {
            if (ReferenceEquals(null, Target) ||
                !handler.AllowSelectField(Target) ||
                !handler.ShouldSelectField(Target))
                return;

            var idField = (handler.Row as IIdRow).IdField;
            var fld = TimelineRow.Fields;

            var listRequest = new ListRequest
            {
                ColumnSelection = ColumnSelection.List,
                EqualityFilter = new Dictionary<string, object>
                {
                    { fld.EntityType.PropertyName, handler.Row.Table },
                    { fld.EntityId.PropertyName, idField.AsObject(handler.Row) ?? -1 }
                }
            };

            var timelineResponse = new TimelineRepository(_context).List(handler.Connection, listRequest);
            var Timeline = timelineResponse.Entities;

            // users might be in another database, in another db server, so we can't simply use a join here
            var userIdList = Timeline.Where(x => x.InsertUserId != null).Select(x => x.InsertUserId.Value).Distinct();
            if (userIdList.Any())
            {
                var u = UserRow.Fields;
                IDictionary<int, string> userDisplayNames;
                using (var connection = _connections.NewFor<UserRow>())
                    userDisplayNames = connection.Query(new SqlQuery()
                            .From(u)
                            .Select(u.UserId)
                            .Select(u.DisplayName)
                            .Where(u.UserId.In(userIdList)))
                        .ToDictionary(x => (int)(x.UserId ?? x.USERID), x => (string)x.DisplayName);

                string s;
                foreach (var x in Timeline)
                    if (x.InsertUserId != null && userDisplayNames.TryGetValue(x.InsertUserId.Value, out s))
                        x.InsertUserDisplayName = s;
            }

            Target.AsObject(handler.Row, Timeline);
        }

        private void SaveTimeline(IUnitOfWork uow, TimelineRow Timeline, string entityType, Int64 entityId, Int64? TimelineId)
        {
            Timeline = Timeline.Clone();
            Timeline.TimelineId = TimelineId;
            Timeline.EntityType = entityType;
            Timeline.EntityId = entityId;
            Timeline.InsertDate = null;
            Timeline.ClearAssignment(TimelineRow.Fields.InsertDate);

            var saveRequest = new SaveRequest<TimelineRow> { Entity = Timeline };

            if (TimelineId == null)
                new TimelineRepository(_context).Create(uow, saveRequest);
            else
                new TimelineRepository(_context).Update(uow, saveRequest);
        }

        private void DeleteTimeline(IUnitOfWork uow, Int64 TimelineId)
        {
            new TimelineRepository(_context).Delete(uow, new DeleteRequest { EntityId = TimelineId });
        }

        private void TimelineListSave(IUnitOfWork uow, string entityType, Int64 entityId, List<TimelineRow> oldList, List<TimelineRow> newList)
        {
            var row = oldList.Count > 0 ? oldList[0] :
                (newList.Count > 0) ? newList[0] : null;

            if (row == null)
                return;

            if (oldList.Count == 0)
            {
                foreach (var Timeline in newList)
                    SaveTimeline(uow, Timeline, entityType, entityId, null);

                return;
            }

            var rowIdField = (row as IIdRow).IdField;

            if (newList.Count == 0)
            {
                foreach (var Timeline in oldList)
                    DeleteTimeline(uow, Convert.ToInt64(rowIdField.AsObject(Timeline)));

                return;
            }

            var oldById = new Dictionary<Int64, TimelineRow>(oldList.Count);
            foreach (var item in oldList)
                oldById[Convert.ToInt64(rowIdField.AsObject(item))] = item;

            var newById = new Dictionary<Int64, TimelineRow>(newList.Count);
            foreach (var item in newList)
            {
                var id = rowIdField.AsObject(item) as long?;
                if (id != null)
                    newById[id.Value] = item;
            }

            foreach (var item in oldList)
            {
                var id = Convert.ToInt64(rowIdField.AsObject(item));
                if (!newById.ContainsKey(id))
                    DeleteTimeline(uow, id);
            }

            foreach (var item in newList)
            {
                var id = rowIdField.AsObject(item) as long?;

                TimelineRow old;
                if (id == null || !oldById.TryGetValue(id.Value, out old))
                    continue;

                bool anyChanges = false;
                foreach (var field in item.GetFields())
                {
                    if (item.IsAssigned(field) &&
                        (field.Flags & FieldFlags.Updatable) == FieldFlags.Updatable &
                        field.IndexCompare(old, item) != 0)
                    {
                        anyChanges = true;
                        break;
                    }
                }

                if (!anyChanges)
                    continue;

                SaveTimeline(uow, item, entityType, entityId, id.Value);
            }

            foreach (var item in newList)
            {
                var id = rowIdField.AsObject(item) as long?;
                if (id == null || !oldById.ContainsKey(id.Value))
                    SaveTimeline(uow, item, entityType, entityId, null);
            }
        }

        public override void OnAfterSave(ISaveRequestHandler handler)
        {
            var newList = Target.AsObject(handler.Row) as List<TimelineRow>;
            if (newList == null)
                return;

            var idField = (handler.Row as IIdRow).IdField;
            var entityId = Convert.ToInt64(idField.AsObject(handler.Row));

            if (handler.IsCreate)
            {
                foreach (var Timeline in newList)
                    SaveTimeline(handler.UnitOfWork, Timeline, handler.Row.Table, entityId, null);

                return;
            }

            var fld = TimelineRow.Fields;
            var listRequest = new ListRequest
            {
                ColumnSelection = ColumnSelection.List,
                EqualityFilter = new Dictionary<string, object>
                {
                    { fld.EntityType.PropertyName, handler.Row.Table },
                    { fld.EntityId.PropertyName, entityId }
                }
            };

            var oldList = new TimelineRepository(_context).List(handler.Connection, listRequest).Entities;
            TimelineListSave(handler.UnitOfWork, handler.Row.Table, entityId, oldList, newList);
        }

        public override void OnBeforeDelete(IDeleteRequestHandler handler)
        {
            if (ReferenceEquals(null, Target) ||
                (Target.Flags & FieldFlags.Updatable) != FieldFlags.Updatable)
                return;

            var idField = (handler.Row as IIdRow).IdField;
            var row = new TimelineRow();
            var fld = TimelineRow.Fields;

            var deleteList = new List<Int64>();
            new SqlQuery()
                    .From(row)
                    .Select(fld.TimelineId)
                    .Where(
                        fld.EntityType == handler.Row.Table &
                        fld.EntityId == Convert.ToInt64(idField.AsObject(handler.Row)))
                    .ForEach(handler.Connection, () =>
                    {
                        deleteList.Add(row.TimelineId.Value);
                    });

            foreach (var id in deleteList)
                DeleteTimeline(handler.UnitOfWork, id);
        }
    }
}