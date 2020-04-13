using System;
using Umbraco.Core.Models;
using Umbraco.Tests.Common.Builders.Extensions;
using Umbraco.Tests.Common.Builders.Interfaces;

namespace Umbraco.Tests.Common.Builders
{
    public class MacroPropertyBuilder
        : ChildBuilderBase<MacroBuilder, IMacroProperty>,
            IWithIdBuilder,
            IWithKeyBuilder,
            IWithAliasBuilder,
            IWithNameBuilder,
            IWithSortOrderBuilder
    {
        private int? _id;
        private Guid? _key;
        private string _alias;
        private string _name;
        private int? _sortOrder;
        private string _editorAlias;

        public MacroPropertyBuilder(MacroBuilder parentBuilder) : base(parentBuilder)
        {
        }

        public MacroPropertyBuilder WithEditorAlias(string editorAlias)
        {
            _editorAlias = editorAlias;
            return this;
        }        

        public override IMacroProperty Build()
        {
            var id = _id ?? 1;
            var name = _name ?? Guid.NewGuid().ToString();
            var alias = _alias ?? name.ToCamelCase();
            var key = _key ?? Guid.NewGuid();
            var sortOrder = _sortOrder ?? 0;
            var editorAlias = _editorAlias ?? string.Empty;

            var macroProperty = new MacroProperty(6, Guid.NewGuid(), alias, name, sortOrder, editorAlias);

            Reset();
            return macroProperty;
        }
        
        protected override void Reset()
        {
            _id = null;
            _key = null;
            _alias = null;
            _name = null;
            _sortOrder = null;
            _editorAlias = null;
        }

        int? IWithIdBuilder.Id
        {
            get => _id;
            set => _id = value;
        }

        Guid? IWithKeyBuilder.Key
        {
            get => _key;
            set => _key = value;
        }

        string IWithAliasBuilder.Alias
        {
            get => _alias;
            set => _alias = value;
        }

        string IWithNameBuilder.Name
        {
            get => _name;
            set => _name = value;
        }

        int? IWithSortOrderBuilder.SortOrder
        {
            get => _sortOrder;
            set => _sortOrder = value;
        }
    }
}
