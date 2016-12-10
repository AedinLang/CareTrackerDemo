using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CareTrackerV1.Models;

namespace CareTrackerV1.ViewModel
{
    public class CareGiverIndexData
    {
        public IEnumerable<CareGiver> CareGivers { get; set; }
        public IEnumerable<Client> Clients { get; set; }
        public IEnumerable<Visit> Visits { get; set; }

        /*public CareGiver CareGiver { get; set; }

        private List<int> _clientsForCareGiver;
        public List<int> ClientsForCareGiver
        {
            get
            {
                if(_clientsForCareGiver== null)
                {
                    _clientsForCareGiver = CareGiver.Clients.Select(c => c.ID).ToList();
                }
                return _clientsForCareGiver;
            }
            set { _clientsForCareGiver = value; }
        }*/
    }
}