namespace AdvanceCRM.Common.Pages
{
    using AdvanceCRM.Administration;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using Serenity;
    using Serenity.Data;
    using Serenity.Services;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;

    [Authorize, Route("Chat")]
    public class ChatController : Controller
    {
        private readonly ISqlConnections _connections;
        private readonly IRequestContext _context;
        private readonly IMemoryCache _cache;
        private readonly IWebHostEnvironment _env;

        public ChatController(ISqlConnections connections, IRequestContext context, IMemoryCache cache,
            IWebHostEnvironment env)
        {
            _connections = connections;
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _cache = cache;
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }

        // Attachments allowed in chat: pictures, PDF and Excel/CSV. Maps extension → kind.
        private const long MaxAttachmentBytes = 20 * 1024 * 1024; // 20 MB

        private static readonly Dictionary<string, string> AllowedAttachments =
            new(StringComparer.OrdinalIgnoreCase)
        {
            [".jpg"] = "image", [".jpeg"] = "image", [".png"] = "image",
            [".gif"] = "image", [".webp"] = "image", [".bmp"] = "image",
            [".pdf"] = "pdf",
            [".xls"] = "excel", [".xlsx"] = "excel", [".csv"] = "excel",
        };

        // Validates an attachment's extension and size, returning its kind. Throws on rejection.
        private static string ValidateAttachment(IFormFile file)
        {
            var ext = Path.GetExtension(file.FileName);
            if (string.IsNullOrEmpty(ext) || !AllowedAttachments.TryGetValue(ext, out var kind))
                throw new ValidationError("Only pictures, PDF and Excel files are allowed.");

            if (file.Length <= 0 || file.Length > MaxAttachmentBytes)
                throw new ValidationError("Each attachment must be between 1 byte and 20 MB.");

            return kind;
        }

        // Saves an uploaded chat attachment under App_Data/upload/chat and returns
        // (relativePath, kind). relativePath is stored in the DB; the public URL is "/upload/" + it.
        private (string path, string kind) SaveAttachment(IFormFile file)
        {
            var kind = ValidateAttachment(file);
            var ext = Path.GetExtension(file.FileName);

            var dir = Path.Combine(_env.ContentRootPath, "App_Data", "upload", "chat");
            Directory.CreateDirectory(dir);

            var safeName = Guid.NewGuid().ToString("N") + ext.ToLowerInvariant();
            using (var stream = new FileStream(Path.Combine(dir, safeName), FileMode.Create))
                file.CopyTo(stream);

            return ("chat/" + safeName, kind);
        }

        private int MyId => Convert.ToInt32(_context.User.GetIdentifier());

        private static string PresenceKey(int userId) => "ChatPresence_" + userId;

        // Mark the current user as online for the next 70 seconds (refreshed on every poll).
        private void TouchPresence()
        {
            _cache.Set(PresenceKey(MyId), DateTime.UtcNow,
                new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(70) });
        }

        private bool IsOnline(int userId) => _cache.TryGetValue(PresenceKey(userId), out _);

        // Typing presence: remembers that "fromUserId is typing to toUserId" for a few seconds.
        private static string TypingKey(int fromUserId, int toUserId) => "ChatTyping_" + fromUserId + "_" + toUserId;

        private void SetTyping(int toUserId)
        {
            _cache.Set(TypingKey(MyId, toUserId), DateTime.UtcNow,
                new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(6) });
        }

        private bool IsTyping(int fromUserId, int toUserId) => _cache.TryGetValue(TypingKey(fromUserId, toUserId), out _);

        private static string AvatarUrl(string userImage)
            => string.IsNullOrEmpty(userImage) ? null : "/upload/" + userImage;

        // Admins (Administration permission) may create groups and switch a user's chat on/off.
        private bool IsAdmin => _context.Permissions.HasPermission("Administration:Read");

        private HashSet<int> DisabledUserIds(IDbConnection connection)
            => connection.Query<int>("SELECT UserId FROM ChatUserSetting WHERE ChatEnabled = 0").ToHashSet();

        private bool IsChatDisabled(IDbConnection connection, int userId)
            => connection.Query<int>(
                "SELECT COUNT(*) FROM ChatUserSetting WHERE UserId = @u AND ChatEnabled = 0",
                new { u = userId }).FirstOrDefault() > 0;

        private bool IsGroupMember(IDbConnection connection, int groupId, int userId)
            => connection.Query<int>(
                "SELECT COUNT(*) FROM ChatGroupMember WHERE GroupId = @g AND UserId = @u",
                new { g = groupId, u = userId }).FirstOrDefault() > 0;

        // True if this group is admins-only (WhatsApp "only admins can send").
        private bool IsGroupAdminsOnly(IDbConnection connection, int groupId)
            => connection.Query<int>(
                "SELECT COUNT(*) FROM ChatGroup WHERE Id = @g AND OnlyAdminsCanSend = 1",
                new { g = groupId }).FirstOrDefault() > 0;

        private sealed class GroupRow
        {
            public int GroupId { get; set; }
            public string Name { get; set; }
            public bool OnlyAdminsCanSend { get; set; }
        }
        private sealed class GroupCountRow { public int GroupId { get; set; } public int Cnt { get; set; } }
        private sealed class MemberRow
        {
            public int UserId { get; set; }
            public string Name { get; set; }
            public string UserImage { get; set; }
        }
        private sealed class GroupMsgRow
        {
            public int Id { get; set; }
            public int SenderId { get; set; }
            public string SenderName { get; set; }
            public string Message { get; set; }
            public DateTime? SentDate { get; set; }
            public string AttachmentPath { get; set; }
            public string AttachmentName { get; set; }
            public string AttachmentType { get; set; }
        }

        // Contact list: every active user (except me) with avatar, online flag and unread count from that user.
        [HttpGet, Route("~/Chat/Contacts")]
        public JsonResult Contacts()
        {
            TouchPresence();
            int me = MyId;
            bool admin = IsAdmin;
            var u = UserRow.Fields;
            var cm = ChatMessageRow.Fields;

            using var connection = _connections.NewFor<UserRow>();

            var disabled = DisabledUserIds(connection);
            bool iAmDisabled = disabled.Contains(me) && !admin;

            // My client is polling, so every message addressed to me has reached my device → mark delivered.
            new SqlUpdate("ChatMessage")
                .Set("IsDelivered", true)
                .Where("ReceiverId = " + me + " AND IsDelivered = 0")
                .Execute(connection, ExpectedRows.Ignore);

            // Only active users appear in chat. As soon as a user's IsActive flag
            // is turned off, the next contacts poll drops them from everyone's list.
            var users = connection.List<UserRow>(q => q
                .Select(u.UserId)
                .Select(u.DisplayName)
                .Select(u.Username)
                .Select(u.UserImage)
                .Select(u.IsActive)
                .Where(u.UserId != me & u.IsActive == 1))
                .Where(x => x.UserId.HasValue)
                .GroupBy(x => x.UserId.Value)
                .Select(g => g.First())
                .ToList();

            var unread = connection.List<ChatMessageRow>(q => q
                .Select(cm.SenderId)
                .Where(cm.ReceiverId == me)
                .Where(new Criteria("IsRead = 0")));

            var unreadBySender = unread
                .Where(x => x.SenderId.HasValue)
                .GroupBy(x => x.SenderId.Value)
                .ToDictionary(g => g.Key, g => g.Count());

            // Non-admins never see chat-disabled users. Admins see everyone, with a flag to toggle access.
            var list = users
                .Where(x => admin || !disabled.Contains(x.UserId.Value))
                .Select(x => new
                {
                    userId = x.UserId.Value,
                    name = string.IsNullOrEmpty(x.DisplayName) ? x.Username : x.DisplayName,
                    avatar = AvatarUrl(x.UserImage),
                    online = IsOnline(x.UserId.Value),
                    unread = unreadBySender.TryGetValue(x.UserId.Value, out var c) ? c : 0,
                    chatEnabled = !disabled.Contains(x.UserId.Value)
                })
                .GroupBy(x => x.userId)
                .Select(g => g.First())
                .OrderByDescending(x => x.unread)
                .ThenByDescending(x => x.online)
                .ThenBy(x => x.name)
                .ToList();

            // Groups I belong to, with per-group unread counts (messages from others past my last read).
            var groups = connection.Query<GroupRow>(
                @"SELECT g.Id AS GroupId, g.Name AS Name, g.OnlyAdminsCanSend AS OnlyAdminsCanSend
                  FROM ChatGroup g
                  INNER JOIN ChatGroupMember m ON m.GroupId = g.Id
                  WHERE g.IsActive = 1 AND m.UserId = @me", new { me }).ToList();

            var groupUnread = connection.Query<GroupCountRow>(
                @"SELECT gm.GroupId AS GroupId, COUNT(*) AS Cnt
                  FROM ChatGroupMessage gm
                  INNER JOIN ChatGroupMember mem ON mem.GroupId = gm.GroupId AND mem.UserId = @me
                  LEFT JOIN ChatGroupRead r ON r.GroupId = gm.GroupId AND r.UserId = @me
                  WHERE gm.SenderId <> @me AND gm.Id > ISNULL(r.LastReadId, 0)
                  GROUP BY gm.GroupId", new { me })
                .ToDictionary(x => x.GroupId, x => x.Cnt);

            var groupList = groups.Select(g => new
            {
                groupId = g.GroupId,
                name = g.Name,
                unread = groupUnread.TryGetValue(g.GroupId, out var c) ? c : 0,
                onlyAdminsCanSend = g.OnlyAdminsCanSend,
                // Members may post unless the group is admins-only and I'm not an admin.
                canSend = !g.OnlyAdminsCanSend || admin
            })
            .OrderByDescending(x => x.unread)
            .ThenBy(x => x.name)
            .ToList();

            // My chat was switched off by an admin: lose 1:1 chat, but keep access to any
            // groups I belong to. With no groups, tell the client to hide the whole widget.
            if (iAmDisabled)
            {
                if (groupList.Count == 0)
                    return new JsonResult(new { disabled = true });

                return new JsonResult(new
                {
                    restricted = true,
                    isAdmin = false,
                    totalUnread = groupList.Sum(x => x.unread),
                    contacts = Array.Empty<object>(),
                    groups = groupList
                });
            }

            return new JsonResult(new
            {
                isAdmin = admin,
                totalUnread = list.Sum(x => x.unread) + groupList.Sum(x => x.unread),
                contacts = list,
                groups = groupList
            });
        }

        // Conversation between me and the other user. Optionally only messages after a given id (for polling).
        // Side effect: marks the other user's messages to me as read.
        [HttpGet, Route("~/Chat/Messages")]
        public JsonResult Messages(int withUserId, int afterId = 0)
        {
            TouchPresence();
            int me = MyId;
            var cm = ChatMessageRow.Fields;

            using var connection = _connections.NewFor<ChatMessageRow>();

            var rows = connection.List<ChatMessageRow>(q => q
                .Select(cm.Id)
                .Select(cm.SenderId)
                .Select(cm.ReceiverId)
                .Select(cm.Message)
                .Select(cm.SentDate)
                .Select(cm.IsRead)
                .Select(cm.IsDelivered)
                .Select(cm.AttachmentPath)
                .Select(cm.AttachmentName)
                .Select(cm.AttachmentType)
                .Where(
                    ((cm.SenderId == me & cm.ReceiverId == withUserId) |
                     (cm.SenderId == withUserId & cm.ReceiverId == me)))
                .Where(cm.Id > afterId)
                .OrderBy(cm.Id));

            // I opened this conversation → the other user's messages to me are now delivered AND read.
            new SqlUpdate("ChatMessage")
                .Set("IsRead", true)
                .Set("IsDelivered", true)
                .Where("SenderId = " + withUserId + " AND ReceiverId = " + me + " AND (IsRead = 0 OR IsDelivered = 0)")
                .Execute(connection, ExpectedRows.Ignore);

            var list = rows.Select(x => new
            {
                id = x.Id.Value,
                mine = x.SenderId == me,
                message = x.Message,
                time = x.SentDate.HasValue ? x.SentDate.Value.ToString("hh:mm tt") : "",
                // Tick state shown to the sender: sent → delivered → read.
                status = x.SenderId == me
                    ? (x.IsRead == true ? "read" : (x.IsDelivered == true ? "delivered" : "sent"))
                    : null,
                attachment = string.IsNullOrEmpty(x.AttachmentPath) ? null : new
                {
                    url = "/upload/" + x.AttachmentPath,
                    name = x.AttachmentName,
                    type = x.AttachmentType
                }
            });

            return new JsonResult(list);
        }

        [HttpPost, Route("~/Chat/Send")]
        public JsonResult Send(int toUserId, string message, List<IFormFile> files)
        {
            TouchPresence();

            files = (files ?? new List<IFormFile>()).Where(f => f != null && f.Length > 0).ToList();
            var text = string.IsNullOrWhiteSpace(message) ? null : message.Trim();

            // A message must carry text and/or at least one attachment.
            if (text == null && files.Count == 0)
                return new JsonResult(new { success = false });

            // Validate the whole batch up front so a bad file doesn't leave partial uploads behind.
            try
            {
                foreach (var f in files)
                    ValidateAttachment(f);
            }
            catch (ValidationError ex)
            {
                return new JsonResult(new { success = false, error = ex.Message });
            }

            int me = MyId;

            // Sending a message means I'm done typing.
            _cache.Remove(TypingKey(me, toUserId));

            using var connection = _connections.NewFor<ChatMessageRow>();

            // Chat-disabled users can neither send nor be messaged.
            if (IsChatDisabled(connection, me) && !IsAdmin)
                return new JsonResult(new { success = false, error = "Your chat has been disabled." });
            if (IsChatDisabled(connection, toUserId))
                return new JsonResult(new { success = false, error = "This user's chat is disabled." });

            DateTime Now() => DateTime.Now;
            var created = new List<object>();

            ChatMessageRow InsertRow(string msgText, string path, string name, string type)
            {
                var row = new ChatMessageRow
                {
                    SenderId = me,
                    ReceiverId = toUserId,
                    Message = msgText,
                    SentDate = Now(),
                    IsRead = false,
                    IsDelivered = false,
                    AttachmentPath = path,
                    AttachmentName = name,
                    AttachmentType = type
                };
                row.Id = (int)connection.InsertAndGetID(row);
                created.Add(new
                {
                    id = row.Id.Value,
                    mine = true,
                    message = row.Message,
                    time = row.SentDate.Value.ToString("hh:mm tt"),
                    status = "sent",
                    attachment = path == null ? null : new
                    {
                        url = "/upload/" + path,
                        name,
                        type
                    }
                });
                return row;
            }

            if (files.Count == 0)
            {
                // Text only.
                InsertRow(text, null, null, null);
            }
            else
            {
                // One message per file; the caption (if any) rides on the first attachment.
                for (int i = 0; i < files.Count; i++)
                {
                    var saved = SaveAttachment(files[i]);
                    InsertRow(i == 0 ? text : null, saved.path,
                        Path.GetFileName(files[i].FileName), saved.kind);
                }
            }

            return new JsonResult(new { success = true, messages = created });
        }

        // Lightweight poll for an open conversation: is the other user typing to me,
        // and what is the delivery/read state of my outgoing messages.
        [HttpGet, Route("~/Chat/ConvState")]
        public JsonResult ConvState(int withUserId)
        {
            TouchPresence();
            int me = MyId;
            var cm = ChatMessageRow.Fields;

            using var connection = _connections.NewFor<ChatMessageRow>();

            // Only my outgoing messages that are not yet read carry a changeable tick state.
            // Anything missing from this list is already read (blue ticks) on the client.
            var rows = connection.List<ChatMessageRow>(q => q
                .Select(cm.Id)
                .Select(cm.IsDelivered)
                .Where(new Criteria("SenderId = " + me + " AND ReceiverId = " + withUserId + " AND IsRead = 0")));

            var statuses = rows.Select(x => new
            {
                id = x.Id.Value,
                delivered = x.IsDelivered == true
            });

            return new JsonResult(new
            {
                typing = IsTyping(withUserId, me),
                statuses
            });
        }

        // Called (throttled) by the client while the user is typing a message.
        [HttpPost, Route("~/Chat/Typing")]
        public JsonResult Typing(int toUserId)
        {
            TouchPresence();
            SetTyping(toUserId);
            return new JsonResult(new { success = true });
        }

        // ----------------------------- Groups -----------------------------

        // Admin creates a group with a name and a set of member user ids (the creator is always added).
        [HttpPost, Route("~/Chat/CreateGroup")]
        public JsonResult CreateGroup(string name, [FromForm] List<int> memberIds, bool onlyAdminsCanSend = false)
        {
            TouchPresence();
            if (!IsAdmin)
                return new JsonResult(new { success = false, error = "Not authorized." });

            name = (name ?? "").Trim();
            if (name.Length == 0)
                return new JsonResult(new { success = false, error = "Group name is required." });

            int me = MyId;
            var members = new HashSet<int>(memberIds ?? new List<int>()) { me };

            using var connection = _connections.NewFor<UserRow>();

            var groupId = connection.Query<int>(
                @"INSERT INTO ChatGroup (Name, CreatedBy, CreatedDate, IsActive, OnlyAdminsCanSend)
                  OUTPUT INSERTED.Id VALUES (@n, @me, @dt, 1, @oa)",
                new { n = name, me, dt = DateTime.Now, oa = onlyAdminsCanSend }).First();

            foreach (var uid in members)
                connection.Execute("INSERT INTO ChatGroupMember (GroupId, UserId) VALUES (@g, @u)",
                    new { g = groupId, u = uid });

            return new JsonResult(new { success = true, groupId, name });
        }

        // Switch a user's chat access on or off (admin only).
        [HttpPost, Route("~/Chat/SetChatAccess")]
        public JsonResult SetChatAccess(int userId, bool enabled)
        {
            TouchPresence();
            if (!IsAdmin)
                return new JsonResult(new { success = false, error = "Not authorized." });

            using var connection = _connections.NewFor<UserRow>();
            connection.Execute(
                @"MERGE ChatUserSetting AS t
                  USING (SELECT @u AS UserId) AS s ON t.UserId = s.UserId
                  WHEN MATCHED THEN UPDATE SET ChatEnabled = @e
                  WHEN NOT MATCHED THEN INSERT (UserId, ChatEnabled) VALUES (@u, @e);",
                new { u = userId, e = enabled });

            return new JsonResult(new { success = true });
        }

        // Messages of a group I belong to. Side effect: marks the group read up to the latest message for me.
        [HttpGet, Route("~/Chat/GroupMessages")]
        public JsonResult GroupMessages(int groupId, int afterId = 0)
        {
            TouchPresence();
            int me = MyId;

            using var connection = _connections.NewFor<ChatMessageRow>();
            if (!IsGroupMember(connection, groupId, me))
                return new JsonResult(Array.Empty<object>());

            var rows = connection.Query<GroupMsgRow>(
                @"SELECT gm.Id, gm.SenderId, gm.Message, gm.SentDate,
                         gm.AttachmentPath, gm.AttachmentName, gm.AttachmentType,
                         ISNULL(NULLIF(us.DisplayName, ''), us.Username) AS SenderName
                  FROM ChatGroupMessage gm
                  INNER JOIN Users us ON us.UserId = gm.SenderId
                  WHERE gm.GroupId = @g AND gm.Id > @after
                  ORDER BY gm.Id", new { g = groupId, after = afterId }).ToList();

            connection.Execute(
                @"MERGE ChatGroupRead AS t
                  USING (SELECT @g AS GroupId, @me AS UserId) AS s
                    ON t.GroupId = s.GroupId AND t.UserId = s.UserId
                  WHEN MATCHED THEN UPDATE SET LastReadId =
                      (SELECT ISNULL(MAX(Id), 0) FROM ChatGroupMessage WHERE GroupId = @g)
                  WHEN NOT MATCHED THEN INSERT (GroupId, UserId, LastReadId)
                      VALUES (@g, @me, (SELECT ISNULL(MAX(Id), 0) FROM ChatGroupMessage WHERE GroupId = @g));",
                new { g = groupId, me });

            var list = rows.Select(x => new
            {
                id = x.Id,
                mine = x.SenderId == me,
                sender = x.SenderId == me ? null : x.SenderName,
                message = x.Message,
                time = x.SentDate.HasValue ? x.SentDate.Value.ToString("hh:mm tt") : "",
                attachment = string.IsNullOrEmpty(x.AttachmentPath) ? null : new
                {
                    url = "/upload/" + x.AttachmentPath,
                    name = x.AttachmentName,
                    type = x.AttachmentType
                }
            });

            return new JsonResult(list);
        }

        [HttpPost, Route("~/Chat/SendGroup")]
        public JsonResult SendGroup(int groupId, string message, List<IFormFile> files)
        {
            TouchPresence();
            int me = MyId;

            using var connection = _connections.NewFor<ChatMessageRow>();
            if (!IsGroupMember(connection, groupId, me))
                return new JsonResult(new { success = false, error = "You are not a member of this group." });

            // Chat-disabled users keep group access (1:1 is blocked elsewhere), so no block here.
            // Admins-only group: only Administration users may post.
            if (!IsAdmin && IsGroupAdminsOnly(connection, groupId))
                return new JsonResult(new { success = false, error = "Only admins can send messages in this group." });

            files = (files ?? new List<IFormFile>()).Where(f => f != null && f.Length > 0).ToList();
            var text = string.IsNullOrWhiteSpace(message) ? null : message.Trim();

            if (text == null && files.Count == 0)
                return new JsonResult(new { success = false });

            try
            {
                foreach (var f in files)
                    ValidateAttachment(f);
            }
            catch (ValidationError ex)
            {
                return new JsonResult(new { success = false, error = ex.Message });
            }

            var created = new List<object>();

            void InsertGroupRow(string msgText, string path, string name, string type)
            {
                var id = connection.Query<int>(
                    @"INSERT INTO ChatGroupMessage
                        (GroupId, SenderId, Message, SentDate, AttachmentPath, AttachmentName, AttachmentType)
                      OUTPUT INSERTED.Id
                      VALUES (@g, @me, @msg, @dt, @p, @n, @t)",
                    new { g = groupId, me, msg = msgText, dt = DateTime.Now, p = path, n = name, t = type }).First();

                created.Add(new
                {
                    id,
                    mine = true,
                    sender = (string)null,
                    message = msgText,
                    time = DateTime.Now.ToString("hh:mm tt"),
                    attachment = path == null ? null : new
                    {
                        url = "/upload/" + path,
                        name,
                        type
                    }
                });
            }

            if (files.Count == 0)
            {
                InsertGroupRow(text, null, null, null);
            }
            else
            {
                for (int i = 0; i < files.Count; i++)
                {
                    var saved = SaveAttachment(files[i]);
                    InsertGroupRow(i == 0 ? text : null, saved.path,
                        Path.GetFileName(files[i].FileName), saved.kind);
                }
            }

            return new JsonResult(new { success = true, messages = created });
        }

        // Members of a group plus its settings — used by the admin "edit group" screen.
        [HttpGet, Route("~/Chat/GroupMembers")]
        public JsonResult GroupMembers(int groupId)
        {
            TouchPresence();
            if (!IsAdmin)
                return new JsonResult(new { success = false, error = "Not authorized." });

            using var connection = _connections.NewFor<UserRow>();
            var info = connection.Query<GroupRow>(
                "SELECT Id AS GroupId, Name, OnlyAdminsCanSend FROM ChatGroup WHERE Id = @g AND IsActive = 1",
                new { g = groupId }).FirstOrDefault();
            if (info == null)
                return new JsonResult(new { success = false, error = "Group not found." });

            var members = connection.Query<MemberRow>(
                @"SELECT DISTINCT us.UserId, ISNULL(NULLIF(us.DisplayName, ''), us.Username) AS Name, us.UserImage
                  FROM ChatGroupMember m
                  INNER JOIN Users us ON us.UserId = m.UserId
                  WHERE m.GroupId = @g
                  ORDER BY Name", new { g = groupId }).ToList();

            return new JsonResult(new
            {
                success = true,
                groupId,
                name = info.Name,
                onlyAdminsCanSend = info.OnlyAdminsCanSend,
                members = members.Select(x => new
                {
                    userId = x.UserId,
                    name = x.Name,
                    avatar = AvatarUrl(x.UserImage)
                })
            });
        }

        // Rename a group / switch its admins-only flag (admin only).
        [HttpPost, Route("~/Chat/UpdateGroup")]
        public JsonResult UpdateGroup(int groupId, string name, bool onlyAdminsCanSend = false)
        {
            TouchPresence();
            if (!IsAdmin)
                return new JsonResult(new { success = false, error = "Not authorized." });

            name = (name ?? "").Trim();
            if (name.Length == 0)
                return new JsonResult(new { success = false, error = "Group name is required." });

            using var connection = _connections.NewFor<UserRow>();
            connection.Execute("UPDATE ChatGroup SET Name = @n, OnlyAdminsCanSend = @oa WHERE Id = @g",
                new { n = name, oa = onlyAdminsCanSend, g = groupId });

            return new JsonResult(new { success = true });
        }

        // Add members to a group, skipping anyone already in it (admin only).
        [HttpPost, Route("~/Chat/AddGroupMembers")]
        public JsonResult AddGroupMembers(int groupId, [FromForm] List<int> memberIds)
        {
            TouchPresence();
            if (!IsAdmin)
                return new JsonResult(new { success = false, error = "Not authorized." });

            using var connection = _connections.NewFor<UserRow>();
            foreach (var uid in (memberIds ?? new List<int>()).Distinct())
                connection.Execute(
                    @"INSERT INTO ChatGroupMember (GroupId, UserId)
                      SELECT @g, @u
                      WHERE NOT EXISTS (SELECT 1 FROM ChatGroupMember WHERE GroupId = @g AND UserId = @u)",
                    new { g = groupId, u = uid });

            return new JsonResult(new { success = true });
        }

        // Remove a single member from a group (admin only).
        [HttpPost, Route("~/Chat/RemoveGroupMember")]
        public JsonResult RemoveGroupMember(int groupId, int userId)
        {
            TouchPresence();
            if (!IsAdmin)
                return new JsonResult(new { success = false, error = "Not authorized." });

            using var connection = _connections.NewFor<UserRow>();
            connection.Execute("DELETE FROM ChatGroupMember WHERE GroupId = @g AND UserId = @u",
                new { g = groupId, u = userId });

            return new JsonResult(new { success = true });
        }

        // Soft-delete a group so it disappears from everyone's list (admin only).
        [HttpPost, Route("~/Chat/DeleteGroup")]
        public JsonResult DeleteGroup(int groupId)
        {
            TouchPresence();
            if (!IsAdmin)
                return new JsonResult(new { success = false, error = "Not authorized." });

            using var connection = _connections.NewFor<UserRow>();
            connection.Execute("UPDATE ChatGroup SET IsActive = 0 WHERE Id = @g", new { g = groupId });

            return new JsonResult(new { success = true });
        }
    }
}
