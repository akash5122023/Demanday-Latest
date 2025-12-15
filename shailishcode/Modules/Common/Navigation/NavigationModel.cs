using AdvanceCRM.Administration;
using AdvanceCRM.Administration.Entities;
using AdvanceCRM.Web.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Serenity;
using Serenity.Abstractions;
using Serenity.Navigation;
using Serenity.Services;
using Serenity.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace AdvanceCRM.Navigation
{
    public partial class NavigationModel
    {
        private readonly IPermissionService _permissionService;
        private readonly ITypeSource _typeSource;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _env;
        private readonly IRequestContext Context;
        public List<NavigationItem> Items { get; private set; }
        public int[] ActivePath { get; set; }

        public NavigationModel()
            : this(
                Dependency.Resolve<IPermissionService>(),
                Dependency.Resolve<ITypeSource>(),
                Dependency.Resolve<IServiceProvider>(),
                Dependency.Resolve<IHttpContextAccessor>(),
                Dependency.Resolve<IRequestContext>(),
                Dependency.Resolve<IWebHostEnvironment>())
        {
        }

        public NavigationModel(
            IPermissionService permissionService,
            ITypeSource typeSource,
            IServiceProvider serviceProvider,
            IHttpContextAccessor httpContextAccessor,
            IRequestContext context,
            IWebHostEnvironment env)
        {
            _permissionService = permissionService;
            _typeSource = typeSource;
            _serviceProvider = serviceProvider;
            _httpContextAccessor = httpContextAccessor;
            Context = context ?? throw new ArgumentNullException(nameof(context));
            _env = env;

            Items = LocalCache.GetLocalStoreOnly(
                "LeftNavigationModel:NavigationItems:" + (Context.User.GetIdentifier() ?? "-1"),
                TimeSpan.Zero,
                UserPermissionRow.Fields.GenerationKey,
                () => NavigationHelper.GetNavigationItems(
                    _permissionService,
                    _typeSource,
                    _serviceProvider,
                    path => path != null && path.StartsWith("~/", StringComparison.Ordinal)
                        ? ToAbsolute(path)
                        : path));

            SetActivePath();
        }

        private string ToAbsolute(string path)
        {
            var basePath = _httpContextAccessor.HttpContext?.Request.PathBase.Value?.TrimEnd('/') ?? string.Empty;
            return basePath + "/" + path.TrimStart('~', '/');
        }

        private void SetActivePath()
        {
            string currentUrl = string.Empty;
            var context = _httpContextAccessor.HttpContext;
            if (context != null)
            {
                var request = context.Request;
                var requestUrl = (request.PathBase + request.Path).ToString();
                currentUrl = requestUrl;
                if (!requestUrl.EndsWith("/") &&
                    string.Equals(request.Path, request.PathBase, StringComparison.OrdinalIgnoreCase))
                {
                    currentUrl += "/";
                }
            }

            int[] currentPath = new int[10];
            int[] bestMatch = null;
            int bestMatchLength = 0;

            foreach (var item in Items)
                SearchActivePath(item, currentUrl, currentPath, 0, ref bestMatch, ref bestMatchLength);

            ActivePath = bestMatch ?? new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
        }

        private void SearchActivePath(NavigationItem link, string currentUrl, int[] currentPath, int depth,
            ref int[] bestMatch, ref int bestMatchLength)
        {
            currentPath[depth + 1] = 0;
            var url = link.Url ?? string.Empty;

            if (url.StartsWith("~/", StringComparison.Ordinal))
                url = ToAbsolute(url);

            if (currentUrl.IndexOf(url, StringComparison.OrdinalIgnoreCase) >= 0 &&
                (bestMatchLength == 0 || url.Length > bestMatchLength))
            {
                bestMatch = (int[])currentPath.Clone();
                bestMatchLength = url.Length;
            }

            if (depth <= 9)
            {
                foreach (var child in link.Children)
                    SearchActivePath(child, currentUrl, currentPath, depth + 1, ref bestMatch, ref bestMatchLength);
            }

            currentPath[depth]++;
        }
    }
}
