namespace Serenity.PropertyGrid
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Serenity.ComponentModel;
    using Serenity.Extensions.DependencyInjection;

    public static class PropertyItemHelper
    {
        public static List<PropertyItem> GetPropertyItemsFor(Type type)
        {
            var provider = Dependency.Resolve<IPropertyItemProvider>();
            return provider.GetPropertyItemsFor(type).ToList();
        }
    }
}
