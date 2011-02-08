﻿using System;
using System.Collections.Generic;
using FubuCore;
using FubuCore.Reflection;
using FubuFastPack.Querying;
using FubuMVC.Core.Urls;

namespace FubuFastPack.JqGrid
{
    public interface IGridColumn
    {
        IEnumerable<IDictionary<string, object>> ToDictionary();
        Action<EntityDTO> CreateFiller(IGridData data, IDisplayFormatter formatter, IUrlRegistry urls);

        IEnumerable<Accessor> SelectAccessors();
        IEnumerable<Accessor> AllAccessors();

        IEnumerable<FilteredProperty> FilteredProperties();

        string GetHeader();
    }
}