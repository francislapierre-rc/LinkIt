﻿#region copyright
// Copyright (c) CBC/Radio-Canada. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
#endregion

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using LinkIt.ConfigBuilders;
using LinkIt.Conventions.Interfaces;
using LinkIt.Shared;

namespace LinkIt.Conventions.DefaultConventions
{
    public class LoadLinkMultiValueWhenIdSuffixMatches : IMultiValueConvention
    {
        public string Name => "Load link multi value when id suffix matches";

        public bool DoesApply(PropertyInfo linkedSourceModelProperty, PropertyInfo linkTargetProperty)
        {
            return linkTargetProperty.MatchLinkedSourceModelPropertyName(linkedSourceModelProperty, "Id", "s");
        }

        public void Apply<TLinkedSource, TLinkTargetProperty, TLinkedSourceModelProperty>(
            LoadLinkProtocolForLinkedSourceBuilder<TLinkedSource> loadLinkProtocolForLinkedSourceBuilder,
            Func<TLinkedSource, List<TLinkedSourceModelProperty>> getLinkedSourceModelProperty,
            Expression<Func<TLinkedSource, List<TLinkTargetProperty>>> getLinkTargetProperty,
            PropertyInfo linkedSourceModelProperty,
            PropertyInfo linkTargetProperty)
        {
            if (typeof(TLinkTargetProperty).DoesImplementILinkedSourceOnceAndOnlyOnce())
                loadLinkProtocolForLinkedSourceBuilder.LoadLinkNestedLinkedSourceById(
                    getLinkedSourceModelProperty,
                    getLinkTargetProperty
                );
            else
                loadLinkProtocolForLinkedSourceBuilder.LoadLinkReferenceById(
                    getLinkedSourceModelProperty,
                    getLinkTargetProperty
                );
        }
    }
}