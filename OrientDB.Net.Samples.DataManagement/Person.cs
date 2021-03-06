﻿using OrientDB.Net.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace OrientDB.Net.Samples.DataManagement
{
    class Person : OrientDBEntity
    {
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<string> FavoriteColors { get; set; }

        public override void Hydrate(IDictionary<string, object> data)
        {
            Age = (int)data?["Age"];
            FirstName = data?["FirstName"]?.ToString();
            LastName = data?["LastName"]?.ToString();
            FavoriteColors = data.ContainsKey("FavoriteColors") ? (data?["FavoriteColors"] as IList<object>).Select(n => n.ToString()).ToList() : new List<string>();
        }
    }
}
