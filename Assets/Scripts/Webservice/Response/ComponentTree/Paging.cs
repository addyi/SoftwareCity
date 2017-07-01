using System;

namespace Webservice.Response.ComponentTree
{
    [Serializable]
    class Paging
    {
        public int pageIndex;
        public int pageSize;
        public int total;

        public override string ToString()
        {
            return string.Format("(ComponentTreePaging: PageIndex={0}, PageSize={1}, TotalComponents={2})", pageIndex, pageSize, total);
        }
    }
}
