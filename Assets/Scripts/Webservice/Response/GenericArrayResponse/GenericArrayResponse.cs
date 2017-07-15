using System;
using System.Collections.Generic;


namespace Webservice.Response.GenericArrayResponse
{
    [Serializable]
    class GenericArrayResponse<T>
    {
        public T array = default(T);
    }
}
