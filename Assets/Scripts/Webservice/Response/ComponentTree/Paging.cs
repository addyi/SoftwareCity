using System;

namespace Webservice.Response.ComponentTree
{
    /// <summary>
    /// This is a POCO for JOSN deserialization of the page nummers of the response the SonarQube API provides
    /// </summary>
    [Serializable]
    class Paging
    {
        /// <summary>
        /// Page number
        /// </summary>
        public int pageIndex = 0;
        /// <summary>
        /// Size of the page (components)
        /// </summary>
        public int pageSize = 0;
        /// <summary>
        /// Total Components on all pages
        /// </summary>
        public int total = 0;

        /// <summary>
        /// As the name suggests this method returns a representation of this class as string
        /// </summary>
        /// <returns>The representation of this class as string</returns>
        public override string ToString()
        {
            return string.Format("(ComponentTreePaging: PageIndex={0}, PageSize={1}, " +
                "TotalComponents={2})", pageIndex, pageSize, total);
        }
    }
}
