﻿using System;
using System.Collections.Generic;

namespace HeterogeneousDataSources {
    public class LoadLinkExpressionForList<TLinkedSource, TReference, TId>: ILoadLinkExpression<TId>, ILoadLinkExpression
    {
        public LoadLinkExpressionForList(
            Func<TLinkedSource, List<TId>> getLookupIdsFunc, 
            Action<TLinkedSource, List<TReference>> linkAction)
        {
            GetLookupIdsFunc = getLookupIdsFunc;
            LinkAction = linkAction;
        }

        #region Load
        public List<TId> GetLookupIds(object linkedSource) {
            //stle: what should we do here? preconditions or defensive?
            if (!(linkedSource is TLinkedSource)) { throw new NotImplementedException(); }

            return GetLookupIds((TLinkedSource)linkedSource);
        }

        public List<TId> GetLookupIds(TLinkedSource linkedSource) {
            return GetLookupIdsFunc(linkedSource);
        }
        private Func<TLinkedSource, List<TId>> GetLookupIdsFunc { get; set; }

        public Type ReferenceType { get { return typeof(TReference); } }
        
        #endregion

        #region Link

        public void Link(object linkedSource, DataContext dataContext) {
            //stle: what should we do here? preconditions or defensive?
            if (!(linkedSource is TLinkedSource)) { throw new NotImplementedException(); }

            Link((TLinkedSource)linkedSource, dataContext);
        }

        public void Link(TLinkedSource linkedSource, DataContext dataContext) {
            var ids = GetLookupIds(linkedSource);
            var reference = dataContext.GetOptionalReferences<TReference, TId>(ids);
            LinkAction(linkedSource, reference);
        }

        private Action<TLinkedSource, List<TReference>> LinkAction { get; set; }
 
	    #endregion
    }
}
