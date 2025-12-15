namespace AdvanceCRM.Contacts.Scripts
{
    using AdvanceCRM.Contacts;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Web;
    using Serenity.Extensions.DependencyInjection;

    [LookupScript("Contacts.SubContactPhoneLookup", Permission = "?")]
    public class SubContactPhoneLookup : RowLookupScript<SubContactsRow>
    {
        public SubContactPhoneLookup(ISqlConnections connections)
            : base(connections)
        {
            IdField = SubContactsRow.Fields.Id.PropertyName;
            TextField = SubContactsRow.Fields.NamePlusPhone.PropertyName;
        }

        public SubContactPhoneLookup()
            : this(Dependency.Resolve<ISqlConnections>())
        {
        }

        protected override void ApplyOrder(SqlQuery query)
        {

        }

        //protected override void PrepareQuery(SqlQuery query)
        //{
        //    base.PrepareQuery(query);

        //    query.Where(SubContactsRow.Fields.NamePlusPhone.IsNotNull());
        //}
    }
}