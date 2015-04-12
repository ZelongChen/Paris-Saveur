using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paris_Saveur.Model
{
    class TransportStationList
    {
        public ObservableCollection<TransportStation> TransportStations { get; private set; }
        public List<AlphaKeyGroup<TransportStation>> AlphaGrouped { get; private set; }

        public TransportStationList()
        {
            TransportStations = new ObservableCollection<TransportStation>();
            AddStations();
            GetAlphaKeyGroup();
        }

        private void AddStations()
        {
            TransportStation station = new TransportStation();
            station.Name = "AAAAA";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "BBBBB";
            TransportStations.Add(station);
        }

        private void GetAlphaKeyGroup()
        {
            var alphaKeyGroup = AlphaKeyGroup<TransportStation>.CreateGroups(
                TransportStations,
                (TransportStation s) => { return s.Name; },
                true);
            AlphaGrouped = alphaKeyGroup.ToList();
        }
    }
}
