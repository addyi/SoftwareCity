using System;

namespace Webservice.Response.ComponentTree
{
    [Serializable]
    class Paging
    {
        public int pageIndex = 0;
        public int pageSize = 0;
        public int total = 0;

        public override string ToString()
        {
            return string.Format("(ComponentTreePaging: PageIndex={0}, PageSize={1}, TotalComponents={2})", pageIndex, pageSize, total);
        }
    }
}
