using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ConfigurationWindow.ConfigurationObserver
{
    public class ConfigurationWindowObserver<T>
    {
        //private Orchestrator observe;

        private List<T> availableMetricList;

        //private readonly string PYRAMIDMETRICFILTER = "percentage";

        public ConfigurationWindowObserver()
        {
            GetAllMetrics();
        }

        private void GetAllMetrics()
        {
            availableMetricList = new List<T>(); //observer.GetMetricList();
        }

        public List<T> GetFixedMetrics()
        {
            List<T> filteredList = availableMetricList
                .Where((T) => { return false; })
                .ToList();
            return filteredList;
        }

        public List<T> GetPercentageMetrics()
        {
            List<T> filteredList = availableMetricList
                .Where((T)=> { return true; })
                .ToList();
            return filteredList;
        }
    }
}
