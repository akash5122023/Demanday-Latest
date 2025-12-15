
namespace AdvanceCRM.Notifications
{
    using AdvanceCRM.Administration;
using AdvanceCRM.Web.Helpers;
    using AdvanceCRM.Common;
    using AdvanceCRM.Common.Repositories;
    using MailChimp.Net.Core;
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Navigation;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Linq;
    using Serenity.Abstractions;
    using Microsoft.AspNetCore.Hosting;
    using AdvanceCRM.Administration.Entities;
    using Serenity.Services;

    public partial class NotificationsModel
    {
        private readonly ISqlConnections _connections;
        private readonly IPermissionService _permissionService;
        private readonly IRequestContext _context;
        private readonly IWebHostEnvironment _env;
        public List<NotificationUsersRow> Items { get; private set; }
        public Int32 count { get; private set; }

        // this function needs to be changed when CRM is deployed as per customer choice of modules
        public NotificationsModel(ISqlConnections connections, IPermissionService permissionService,
            IRequestContext context, IWebHostEnvironment env)
        {
            _connections = connections ?? throw new ArgumentNullException(nameof(connections));
            _permissionService = permissionService ?? throw new ArgumentNullException(nameof(permissionService));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _env = env;

            if (!_permissionService.HasPermission("Notifications:Read"))
            {
                Items = new List<NotificationUsersRow>();
                return;
            }

            Items = LocalCache.GetLocalStoreOnly(
                "NotificationsModel:NotificationsItems:" + (_context.User.GetIdentifier() ?? "-1"),
                TimeSpan.FromSeconds(1),
                UserPermissionRow.Fields.GenerationKey,
                NotificationsList);
        }

        private List<NotificationUsersRow> NotificationsList()
        {
            var request = new Serenity.Services.ListRequest();
            request.Take = 20;

            using var connection = _connections.NewFor<NotificationUsersRow>();

            var repo = new NotificationUsersRepository(_context);
            var list = repo.List(connection, request).Entities.ToList();

            var n = NotificationUsersRow.Fields;
            var userId = Convert.ToInt32(_context.User.GetIdentifier());
            count = connection.Count<NotificationUsersRow>(n.UserId == userId && n.IsChecked != 1);
            

            return list;
        }
    }
}