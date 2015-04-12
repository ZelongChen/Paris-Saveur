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
            station.Name = "Nation";
            station.Latitude = "48.8481112315756";
            station.Longitude = "2.39800401279774";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Concorde";
            station.Latitude = "48.8656780964177";
            station.Longitude = "2.32119377180186";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Bastille";
            station.Latitude = "48.8529756746542";
            station.Longitude = "2.36921882444334";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Louvre-Rivoli";
            station.Latitude = "48.8608798575937";
            station.Longitude = "2.3409732758179";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Bérault";
            station.Latitude = "48.8453687501573";
            station.Longitude = "2.42824450962048";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Champs-Elysées-Clémenceau";
            station.Latitude = "48.8677440260687";
            station.Longitude = "2.31412278037414";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Reuilly-Diderot";
            station.Latitude = "48.8473528769588";
            station.Longitude = "2.38584254004024";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "George V";
            station.Latitude = "48.8720456194263";
            station.Longitude = "2.30076918564495";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Argentine";
            station.Latitude = "48.8756724859879";
            station.Longitude = "2.28944416448144";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Franklin-Roosevelt";
            station.Latitude = "48.8690104279376";
            station.Longitude = "2.31025318089602";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Palais-Royal (Musée du Louvre)";
            station.Latitude = "48.8623718215243";
            station.Longitude = "2.33657360073643";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Pont de Neuilly";
            station.Latitude = "48.8855061094181";
            station.Longitude = "2.25852747019735";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Saint-Mandé";
            station.Latitude = "48.8462382566207";
            station.Longitude = "2.41899982810925";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Chêteau de Vincennes";
            station.Latitude = "48.8443251443685";
            station.Longitude = "2.44055234621182";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Esplanade de la Défense";
            station.Latitude = "48.8883580601661";
            station.Longitude = "2.2499372128628";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Chêtelet";
            station.Latitude = "48.8585696724797";
            station.Longitude = "2.3479332458353";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Porte Maillot";
            station.Latitude = "48.8780061762789";
            station.Longitude = "2.28246564000133";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Charles de Gaulle-Etoile";
            station.Latitude = "48.8739310942679";
            station.Longitude = "2.2951272516501";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Tuileries";
            station.Latitude = "48.8647801625";
            station.Longitude = "2.32909495442942";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Les Sablons (Jardin d'acclimatation)";
            station.Latitude = "48.8812991854911";
            station.Longitude = "2.27191517601779";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Porte de Vincennes";
            station.Latitude = "48.8470165184191";
            station.Longitude = "2.41081688233476";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Gare de Lyon";
            station.Latitude = "48.8455597870834";
            station.Longitude = "2.37344920496318";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Saint-Paul (Le Marais)";
            station.Latitude = "48.855134518235";
            station.Longitude = "2.36133433906766";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Hôtel de Ville";
            station.Latitude = "48.8573559271695";
            station.Longitude = "2.35207356685871";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "La Défense (Grande Arche)";
            station.Latitude = "48.8918267548365";
            station.Longitude = "2.23799204321561";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Victor Hugo";
            station.Latitude = "48.869864912484";
            station.Longitude = "2.28522620725043";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Pigalle";
            station.Latitude = "48.8824225284175";
            station.Longitude = "2.33725468210153";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "La Chapelle";
            station.Latitude = "48.8843886983581";
            station.Longitude = "2.35927656427672";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Philippe Auguste";
            station.Latitude = "48.8583788414699";
            station.Longitude = "2.3897347327397";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Place de Clichy";
            station.Latitude = "48.8834556515954";
            station.Longitude = "2.32737499341035";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Père-Lachaise";
            station.Latitude = "48.8631612050639";
            station.Longitude = "2.38726040018435";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Porte Dauphine (Maréchal de Lattre de Tassigny)";
            station.Latitude = "48.8717922164175";
            station.Longitude = "2.27486996119569";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Barbès-Rochechouart";
            station.Latitude = "48.883680109279";
            station.Longitude = "2.34953280365193";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Belleville";
            station.Latitude = "48.8722873205871";
            station.Longitude = "2.37673767732246";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Colonel Fabien";
            station.Latitude = "48.8781849817921";
            station.Longitude = "2.37017454663635";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Villiers";
            station.Latitude = "48.8813242939446";
            station.Longitude = "2.31659676150881";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Blanche";
            station.Latitude = "48.8837705188317";
            station.Longitude = "2.33248514827409";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Stalingrad";
            station.Latitude = "48.8841892942087";
            station.Longitude = "2.36703044197456";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Anvers";
            station.Latitude = "48.8828716901805";
            station.Longitude = "2.34416357280039";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Monceau";
            station.Latitude = "48.8805768907484";
            station.Longitude = "2.3094159461648";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Alexandre-Dumas";
            station.Latitude = "48.8564263483252";
            station.Longitude = "2.39455425765499";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Avron";
            station.Latitude = "48.8511938192132";
            station.Longitude = "2.39823914082312";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Jaurès";
            station.Latitude = "48.8827685467488";
            station.Longitude = "2.36994579899074";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Ménilmontant";
            station.Latitude = "48.865705787623";
            station.Longitude = "2.3844293751632";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Ternes";
            station.Latitude = "48.8782280405076";
            station.Longitude = "2.29812143258218";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Courcelles";
            station.Latitude = "48.8792721516315";
            station.Longitude = "2.30329854668248";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Couronnes";
            station.Latitude = "48.8693291694965";
            station.Longitude = "2.38053636616172";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Rome";
            station.Latitude = "48.882070833937";
            station.Longitude = "2.32039833930998";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Péreire";
            station.Latitude = "48.8850310612536";
            station.Longitude = "2.29713511150616";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Sentier";
            station.Latitude = "48.8673503814313";
            station.Longitude = "2.34756723541508";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "République";
            station.Latitude = "48.8672579435982";
            station.Longitude = "2.36403730363905";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Réaumur-Sébastopol";
            station.Latitude = "48.8662803585612";
            station.Longitude = "2.35248479683784";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Anatole-France";
            station.Latitude = "48.8920186263288";
            station.Longitude = "2.28551744479448";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Porte de Bagnolet";
            station.Latitude = "48.8643365354836";
            station.Longitude = "2.40882537603772";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Porte de Champerret";
            station.Latitude = "48.885739159692";
            station.Longitude = "2.29186062067553";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Quatre Septembre";
            station.Latitude = "48.869660572192";
            station.Longitude = "2.33632828164171";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Louise Michel";
            station.Latitude = "48.889269622246";
            station.Longitude = "2.2880551248039";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Arts-et-Métiers";
            station.Latitude = "48.8655518636347";
            station.Longitude = "2.35610815512031";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Wagram";
            station.Latitude = "48.8837124760373";
            station.Longitude = "2.3055577575143";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Pont de Levallois-Bécon";
            station.Latitude = "48.8973099051795";
            station.Longitude = "2.2807686428069";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Saint-Lazare";
            station.Latitude = "48.8756815064196";
            station.Longitude = "2.32559158481995";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Gallieni (Parc de Bagnolet)";
            station.Latitude = "48.8632625265005";
            station.Longitude = "2.41597536849995";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Gambetta";
            station.Latitude = "48.8647827006878";
            station.Longitude = "2.39844583377186";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Parmentier";
            station.Latitude = "48.8652598566731";
            station.Longitude = "2.37475708171676";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Europe";
            station.Latitude = "48.8790604392127";
            station.Longitude = "2.32297463240589";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Bourse";
            station.Latitude = "48.8687617925963";
            station.Longitude = "2.3406741288121";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Rue Saint-Maur";
            station.Latitude = "48.8640894057464";
            station.Longitude = "2.38089978855211";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Opéra";
            station.Latitude = "48.870720976292";
            station.Longitude = "2.33225474357295";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Temple";
            station.Latitude = "48.8667641678145";
            station.Longitude = "2.36157132703273";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Malesherbes";
            station.Latitude = "48.8825364728921";
            station.Longitude = "2.31100924300551";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Havre-Caumartin";
            station.Latitude = "48.8734618906704";
            station.Longitude = "2.32848048447347";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Saint-Fargeau";
            station.Latitude = "48.8716998109711";
            station.Longitude = "2.40429872420218";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Pelleport";
            station.Latitude = "48.8682233476073";
            station.Longitude = "2.4013243805441";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Porte des Lilas";
            station.Latitude = "48.8769382235089";
            station.Longitude = "2.40637656692978";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Simplon";
            station.Latitude = "48.8941234446659";
            station.Longitude = "2.34724544444302";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Vavin";
            station.Latitude = "48.842059857582";
            station.Longitude = "2.32886705043721";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Cité";
            station.Latitude = "48.8556848502862";
            station.Longitude = "2.34620292952871";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Porte d'Orléans (Général Leclerc)";
            station.Latitude = "48.8228170602349";
            station.Longitude = "2.325072625148";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Saint-Sulpice";
            station.Latitude = "48.8511463550549";
            station.Longitude = "2.33096285636646";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Saint-Placide";
            station.Latitude = "48.8470118401686";
            station.Longitude = "2.32705511135513";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Strasbourg-Saint-Denis";
            station.Latitude = "48.8696323651874";
            station.Longitude = "2.35450206308924";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Montparnasse-Bienvenue";
            station.Latitude = "48.8441895085267";
            station.Longitude = "2.3244412324337";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Saint-Germain des Prés";
            station.Latitude = "48.8534472377383";
            station.Longitude = "2.33304632604486";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Gare de l'Est (Verdun)";
            station.Latitude = "48.8766958360248";
            station.Longitude = "2.3579925105504";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Marcadet-Poissonniers";
            station.Latitude = "48.8915439306305";
            station.Longitude = "2.34937111538191";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Etienne Marcel";
            station.Latitude = "48.8637103664695";
            station.Longitude = "2.3489831961271";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Chêteau Rouge";
            station.Latitude = "48.8870862830504";
            station.Longitude = "2.34937007325137";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Saint-Michel";
            station.Latitude = "48.8535999078857";
            station.Longitude = "2.34399624109401";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Chêteau d'Eau";
            station.Latitude = "48.8724541509199";
            station.Longitude = "2.35605616813029";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Mouton-Duvernet";
            station.Latitude = "48.8321915992696";
            station.Longitude = "2.33040699262932";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Alésia";
            station.Latitude = "48.8280660196864";
            station.Longitude = "2.32682742005083";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Gare du Nord";
            station.Latitude = "48.8797430642253";
            station.Longitude = "2.35460081047223";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Les Halles";
            station.Latitude = "48.8620119707205";
            station.Longitude = "2.3464764519062";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Odéon";
            station.Latitude = "48.8521980452304";
            station.Longitude = "2.33878000304498";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Denfert-Rochereau";
            station.Latitude = "48.8342768128446";
            station.Longitude = "2.33220379730478";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Porte de Clignancourt";
            station.Latitude = "48.8979701704071";
            station.Longitude = "2.34398838343329";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Raspail";
            station.Latitude = "48.8391569814967";
            station.Longitude = "2.33047415116293";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Mairie de Montrouge";
            station.Latitude = "48.8184663371133";
            station.Longitude = "2.31964321861782";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Place d'Italie";
            station.Latitude = "48.8312826555064";
            station.Longitude = "2.3554558703645";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Bobigny-Pablo-Picasso";
            station.Latitude = "48.9071473125515";
            station.Longitude = "2.44945750453114";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Porte de Pantin";
            station.Latitude = "48.8885839809718";
            station.Longitude = "2.39242287571854";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Eglise de Pantin";
            station.Latitude = "48.893632527974";
            station.Longitude = "2.41290026603005";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Saint-Marcel";
            station.Latitude = "48.8394422789206";
            station.Longitude = "2.36164021412343";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Oberkampf";
            station.Latitude = "48.8640302101823";
            station.Longitude = "2.36918477255233";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Laumière";
            station.Latitude = "48.8849316105461";
            station.Longitude = "2.37909124689983";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Bobigny-Pantin (Raymond Queneau)";
            station.Latitude = "48.895799232549";
            station.Longitude = "2.42485729839759";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Bréguet-Sabin";
            station.Latitude = "48.8567679495405";
            station.Longitude = "2.37077385838438";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Campo-Formio";
            station.Latitude = "48.8355512166754";
            station.Longitude = "2.3587520951914";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Richard-Lenoir";
            station.Latitude = "48.8606859919957";
            station.Longitude = "2.3724109318802";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Hoche";
            station.Latitude = "48.8914993837642";
            station.Longitude = "2.40312496065607";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Ourcq";
            station.Latitude = "48.8866187923769";
            station.Longitude = "2.38548413638487";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Gare d'Austerlitz";
            station.Latitude = "48.8434142241437";
            station.Longitude = "2.36418839286764";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Quai de la Rapée";
            station.Latitude = "48.8459660324064";
            station.Longitude = "2.36691317973831";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Jacques-Bonsergent";
            station.Latitude = "48.8707008232695";
            station.Longitude = "2.36057863558727";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Picpus";
            station.Latitude = "48.8453231146139";
            station.Longitude = "2.40178631376068";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Sèvres-Lecourbe";
            station.Latitude = "48.8449961508568";
            station.Longitude = "2.31086483004415";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Bel-Air";
            station.Latitude = "48.8421778264756";
            station.Longitude = "2.40114243430574";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Bercy";
            station.Latitude = "48.8401922444034";
            station.Longitude = "2.37947725726605";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Kléber";
            station.Latitude = "48.8713061490878";
            station.Longitude = "2.29333102089621";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Bir-Hakeim (Grenelle)";
            station.Latitude = "48.8543272185523";
            station.Longitude = "2.28881010373611";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Dugommier";
            station.Latitude = "48.8388761516785";
            station.Longitude = "2.38934738757029";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Dupleix";
            station.Latitude = "48.8507426501802";
            station.Longitude = "2.2924632268245";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Cambronne";
            station.Latitude = "48.8476991081708";
            station.Longitude = "2.30203914366926";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Nationale";
            station.Latitude = "48.8328812886788";
            station.Longitude = "2.36190940801619";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Glacière";
            station.Latitude = "48.8308614053467";
            station.Longitude = "2.34455143330341";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "La Motte-Picquet-Grenelle";
            station.Latitude = "48.8490189283497";
            station.Longitude = "2.29776216070793";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Edgar-Quinet";
            station.Latitude = "48.8405677262043";
            station.Longitude = "2.32640281522276";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Passy";
            station.Latitude = "48.8575343825138";
            station.Longitude = "2.28559270581709";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Pasteur";
            station.Latitude = "48.8420128040576";
            station.Longitude = "2.31329004383549";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Chevaleret";
            station.Latitude = "48.8345787984256";
            station.Longitude = "2.36693389907091";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Trocadéro";
            station.Latitude = "48.8626669058792";
            station.Longitude = "2.28726306155326";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Saint-Jacques";
            station.Latitude = "48.8329916917758";
            station.Longitude = "2.33668289664434";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Quai de la Gare";
            station.Latitude = "48.8376146757853";
            station.Longitude = "2.37385211568049";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Daumesnil (Félix Eboué)";
            station.Latitude = "48.8393764631249";
            station.Longitude = "2.39578806041564";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Corvisart";
            station.Latitude = "48.8296027449711";
            station.Longitude = "2.34937026345836";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Boissière";
            station.Latitude = "48.8668561547603";
            station.Longitude = "2.290038073445";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Villejuif-Paul Vaillant Couturier (Hôpital Paul Brousse)";
            station.Latitude = "48.7959941323931";
            station.Longitude = "2.36823145191568";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Villejuif-Louis Aragon";
            station.Latitude = "48.7866380111198";
            station.Longitude = "2.36711052738164";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Pierre et Marie Curie";
            station.Latitude = "48.8159171816003";
            station.Longitude = "2.37787881648387";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "La Courneuve-8-Mai-1945";
            station.Latitude = "48.9205145066856";
            station.Longitude = "2.41022577912019";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Poissonnière";
            station.Latitude = "48.877353095288";
            station.Longitude = "2.34938142395259";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Porte d'Ivry";
            station.Latitude = "48.8213573623075";
            station.Longitude = "2.36930823722324";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Chêteau Landon";
            station.Latitude = "48.8784565668311";
            station.Longitude = "2.36205371624889";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Le Kremlin-Bicêtre";
            station.Latitude = "48.8103851251294";
            station.Longitude = "2.36183068309113";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Jussieu";
            station.Latitude = "48.8459684322046";
            station.Longitude = "2.35480739269421";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Cadet";
            station.Latitude = "48.8758257180381";
            station.Longitude = "2.34334512457327";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Louis Blanc";
            station.Latitude = "48.880900428151";
            station.Longitude = "2.36510721826433";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Censier-Daubenton";
            station.Latitude = "48.8405943000016";
            station.Longitude = "2.35175561294668";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Riquet";
            station.Latitude = "48.8883932319013";
            station.Longitude = "2.37443310888499";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Pont Neuf";
            station.Latitude = "48.8585341022857";
            station.Longitude = "2.34211723866599";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Porte d'Italie";
            station.Latitude = "48.8187170251155";
            station.Longitude = "2.36050083280536";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Pont Marie (Cité des Arts)";
            station.Latitude = "48.8534635856947";
            station.Longitude = "2.35738397280916";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Aubervilliers Pantin (4 Chemins)";
            station.Latitude = "48.9038980475294";
            station.Longitude = "2.39248007867247";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Mairie d'Ivry";
            station.Latitude = "48.8112145354902";
            station.Longitude = "2.38350892873268";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Villejuif-Léo Lagrange";
            station.Latitude = "48.8037696632826";
            station.Longitude = "2.36390930638904";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Fort d'Aubervilliers";
            station.Latitude = "48.9140567451465";
            station.Longitude = "2.403576334511";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Maison Blanche";
            station.Latitude = "48.8228787750632";
            station.Longitude = "2.35807989429085";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Pyramides";
            station.Latitude = "48.8664969870385";
            station.Longitude = "2.33409421525909";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Chaussée d'Antin (La Fayette)";
            station.Latitude = "48.8729858246651";
            station.Longitude = "2.33342622405285";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Les Gobelins";
            station.Latitude = "48.8361274397525";
            station.Longitude = "2.35208111783557";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Tolbiac";
            station.Latitude = "48.8261594539113";
            station.Longitude = "2.35701946618912";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Porte de la Villette";
            station.Latitude = "48.8975291568914";
            station.Longitude = "2.3856442301273";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Le Peletier";
            station.Latitude = "48.8749630580261";
            station.Longitude = "2.3401567908492";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Sully-Morland";
            station.Latitude = "48.8507935947421";
            station.Longitude = "2.36123699371429";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Place Monge (Jardin des Plantes)";
            station.Latitude = "48.8431646927668";
            station.Longitude = "2.35211036241509";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Porte de Choisy";
            station.Latitude = "48.8199024461616";
            station.Longitude = "2.36496559829081";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Crimée";
            station.Latitude = "48.8902887750446";
            station.Longitude = "2.37677864892536";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Corentin-Cariou";
            station.Latitude = "48.8946815419881";
            station.Longitude = "2.38230219397424";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Bolivar";
            station.Latitude = "48.8807901228284";
            station.Longitude = "2.37415512666264";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Danube";
            station.Latitude = "48.8819510181782";
            station.Longitude = "2.39326067585738";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Botzaris";
            station.Latitude = "48.8796070889331";
            station.Longitude = "2.3894155696532";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Pré-Saint-Gervais";
            station.Latitude = "48.8801599321124";
            station.Longitude = "2.39858662186976";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Buttes-Chaumont";
            station.Latitude = "48.8778039560078";
            station.Longitude = "2.38121109973598";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Place des Fêtes";
            station.Latitude = "48.8772239477293";
            station.Longitude = "2.39276505025089";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Félix Faure";
            station.Latitude = "48.842824567674";
            station.Longitude = "2.29222496622051";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Faidherbe-Chaligny";
            station.Latitude = "48.8500676680027";
            station.Longitude = "2.38442877074163";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Montgallet";
            station.Latitude = "48.8441427311209";
            station.Longitude = "2.38962501284842";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Porte Dorée";
            station.Latitude = "48.8354972528246";
            station.Longitude = "2.40618521221493";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Michel Bizot";
            station.Latitude = "48.8369910376529";
            station.Longitude = "2.40291960687004";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Commerce";
            station.Latitude = "48.844613671994";
            station.Longitude = "2.29380298016914";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "La Tour-Maubourg";
            station.Latitude = "48.8572278606496";
            station.Longitude = "2.30979622582735";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Ecole Militaire";
            station.Latitude = "48.8542609215541";
            station.Longitude = "2.30543956053901";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Pointe du Lac";
            station.Latitude = "48.7681110763834";
            station.Longitude = "2.46565028903882";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Saint-Sébastien-Froissart";
            station.Latitude = "48.8609749971871";
            station.Longitude = "2.36726229502294";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Filles du Calvaire";
            station.Latitude = "48.8630781763554";
            station.Longitude = "2.36674588358473";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Chemin Vert";
            station.Latitude = "48.8575865444645";
            station.Longitude = "2.36803669494837";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Ledru-Rollin";
            station.Latitude = "48.8511941653636";
            station.Longitude = "2.37593173701289";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Bonne Nouvelle";
            station.Latitude = "48.8705767535819";
            station.Longitude = "2.34849429371092";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Grands Boulevards";
            station.Latitude = "48.871574743593";
            station.Longitude = "2.34289503198465";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Richelieu-Drouot";
            station.Latitude = "48.8720062389613";
            station.Longitude = "2.33991139061041";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Porte de Charenton";
            station.Latitude = "48.8326603464821";
            station.Longitude = "2.40046363844978";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Liberté";
            station.Latitude = "48.8256371117279";
            station.Longitude = "2.40741080542612";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Maisons-Alfort-Stade";
            station.Latitude = "48.8082253473523";
            station.Longitude = "2.43627582105113";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Charenton-Ecoles";
            station.Latitude = "48.8214898191455";
            station.Longitude = "2.4138567623311";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Ecole Vétérinaire de Maisons-Alfort";
            station.Latitude = "48.8150763201297";
            station.Longitude = "2.42163176673066";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Invalides";
            station.Latitude = "48.8610934676205";
            station.Longitude = "2.31464335534643";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Balard";
            station.Latitude = "48.8368149888059";
            station.Longitude = "2.27868334645027";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Créteil-L'Echat (Hôpital Henri Mondor)";
            station.Latitude = "48.7966012975784";
            station.Longitude = "2.44953152437607";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Créteil-Université";
            station.Latitude = "48.78972431608";
            station.Longitude = "2.45083610639066";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Maisons-Alfort-Les Juilliottes";
            station.Latitude = "48.8025004418306";
            station.Longitude = "2.44618361855945";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Boucicaut";
            station.Latitude = "48.8410253553216";
            station.Longitude = "2.28792386733086";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Lourmel";
            station.Latitude = "48.83866812903";
            station.Longitude = "2.28224848838409";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Madeleine";
            station.Latitude = "48.869507127629";
            station.Longitude = "2.32468019959151";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Créteil-Préfecture (Hôtel de Ville)";
            station.Latitude = "48.7797572578445";
            station.Longitude = "2.45931429107882";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Billancourt";
            station.Latitude = "48.8320876442825";
            station.Longitude = "2.23816077687551";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Saint-Philippe du Roule";
            station.Latitude = "48.8724706657937";
            station.Longitude = "2.31076910123035";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Ranelagh";
            station.Latitude = "48.8555044030019";
            station.Longitude = "2.26995917825576";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Buzenval";
            station.Latitude = "48.8517674745051";
            station.Longitude = "2.40118148387264";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Alma-Marceau";
            station.Latitude = "48.8642986710466";
            station.Longitude = "2.30125141044992";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Robespierre";
            station.Latitude = "48.8556808081969";
            station.Longitude = "2.42365908548301";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Pont de Sèvres";
            station.Latitude = "48.8296903380227";
            station.Longitude = "2.23052843419233";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "La Muette";
            station.Latitude = "48.8580950496832";
            station.Longitude = "2.27409644811061";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Exelmans";
            station.Latitude = "48.8429069785359";
            station.Longitude = "2.26014439788089";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Voltaire (Léon Blum)";
            station.Latitude = "48.85766372576";
            station.Longitude = "2.3800362967955";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Charonne";
            station.Latitude = "48.8546420474447";
            station.Longitude = "2.38503219333482";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Jasmin";
            station.Latitude = "48.8524385192226";
            station.Longitude = "2.26793392300095";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Mairie de Montreuil";
            station.Latitude = "48.862280986339";
            station.Longitude = "2.44182767912771";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Iéna";
            station.Latitude = "48.8643501996472";
            station.Longitude = "2.29404526378309";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Michel-Ange-Molitor";
            station.Latitude = "48.845056129032";
            station.Longitude = "2.26191142327976";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Porte de Saint-Cloud";
            station.Latitude = "48.8375653344736";
            station.Longitude = "2.25559127444789";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Croix-de-Chavaux (Jacques Duclos)";
            station.Latitude = "48.857999073258";
            station.Longitude = "2.4358668956747";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Michel-Ange-Auteuil";
            station.Latitude = "48.8479245927851";
            station.Longitude = "2.26423590578241";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Porte de Montreuil";
            station.Latitude = "48.8533798778138";
            station.Longitude = "2.41045818741635";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Miromesnil";
            station.Latitude = "48.873532140616";
            station.Longitude = "2.31557802486618";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Saint-Ambroise";
            station.Latitude = "48.8616474242992";
            station.Longitude = "2.3731471407723";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Saint-Augustin";
            station.Latitude = "48.8743867602127";
            station.Longitude = "2.32072783196473";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Maraichers";
            station.Latitude = "48.8526004024001";
            station.Longitude = "2.40638498956613";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Marcel Sembat";
            station.Latitude = "48.8338085869802";
            station.Longitude = "2.24348061441739";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Rue des Boulets";
            station.Latitude = "48.8522227131835";
            station.Longitude = "2.38911565092718";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Rue de la Pompe (Avenue Georges Mandel)";
            station.Latitude = "48.8642533844034";
            station.Longitude = "2.27791676202884";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Ségur";
            station.Latitude = "48.8468647020691";
            station.Longitude = "2.30726884757892";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Maubert-Mutualité";
            station.Latitude = "48.8497169401635";
            station.Longitude = "2.34892555849187";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Sèvres-Babylone";
            station.Latitude = "48.8509392884307";
            station.Longitude = "2.32611465482228";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Vaneau";
            station.Latitude = "48.84870084962";
            station.Longitude = "2.32123988193857";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Mabillon";
            station.Latitude = "48.8528451336544";
            station.Longitude = "2.33514371851606";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Eglise d'Auteuil";
            station.Latitude = "48.8478832165791";
            station.Longitude = "2.27029589263431";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Javel-André-Citroen";
            station.Latitude = "48.845963880484";
            station.Longitude = "2.27781512635377";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Boulogne-Jean-Jaurès";
            station.Latitude = "48.8422801435297";
            station.Longitude = "2.23886297536727";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Duroc";
            station.Latitude = "48.8468487688959";
            station.Longitude = "2.31693731083408";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Cluny-La Sorbonne";
            station.Latitude = "48.8507957557042";
            station.Longitude = "2.34503088836522";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Mirabeau";
            station.Latitude = "48.8470848589048";
            station.Longitude = "2.27307491492201";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Porte d'Auteuil";
            station.Latitude = "48.8482259804111";
            station.Longitude = "2.25769888566092";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Cardinal-Lemoine";
            station.Latitude = "48.8467058690602";
            station.Longitude = "2.35133517897259";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Avenue Emile-Zola";
            station.Latitude = "48.8468521519497";
            station.Longitude = "2.29544882793447";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Charles Michels";
            station.Latitude = "48.8463629755008";
            station.Longitude = "2.28563106588287";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Boulogne Pont de Saint-Cloud";
            station.Latitude = "48.8406801631219";
            station.Longitude = "2.22831366172434";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Chardon-Lagache";
            station.Latitude = "48.845104139567";
            station.Longitude = "2.26693605903225";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Jourdain";
            station.Latitude = "48.8752482946249";
            station.Longitude = "2.38932943415264";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Goncourt (Hôpital Saint-Louis)";
            station.Latitude = "48.870015355247";
            station.Longitude = "2.37076879902146";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Telegraphe";
            station.Latitude = "48.8755224492456";
            station.Longitude = "2.39867651114495";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Rambuteau";
            station.Latitude = "48.8611933956416";
            station.Longitude = "2.35328695987071";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Pyrenees";
            station.Latitude = "48.8738211723367";
            station.Longitude = "2.3848863721979";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Mairie des Lilas";
            station.Latitude = "48.8800510617726";
            station.Longitude = "2.41566020606422";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Solférino";
            station.Latitude = "48.8585333595082";
            station.Longitude = "2.3230892487872";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Mairie d'Issy";
            station.Latitude = "48.8243465260184";
            station.Longitude = "2.27356582960064";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Vaugirard (Adolphe Chérioux)";
            station.Latitude = "48.8394392812395";
            station.Longitude = "2.30107804247392";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Lamarck-Caulaincourt";
            station.Latitude = "48.8897381077178";
            station.Longitude = "2.33914908667411";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Notre-Dame des Champs";
            station.Latitude = "48.84478304833";
            station.Longitude = "2.32844448329272";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Assemblée Nationale";
            station.Latitude = "48.8614539042948";
            station.Longitude = "2.32030969050735";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Falguière";
            station.Latitude = "48.844323383002";
            station.Longitude = "2.31756467618059";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Volontaires";
            station.Latitude = "48.8414184426777";
            station.Longitude = "2.30799364938666";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Saint-Georges";
            station.Latitude = "48.8785939552362";
            station.Longitude = "2.33782696891701";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Abbesses";
            station.Latitude = "48.8843997149084";
            station.Longitude = "2.33839938417104";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Notre-Dame de Lorette";
            station.Latitude = "48.8767964877418";
            station.Longitude = "2.33902598479516";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Front Populaire";
            station.Latitude = "48.9066665878747";
            station.Longitude = "2.36523013585437";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Porte de Versailles";
            station.Latitude = "48.832253452749";
            station.Longitude = "2.28786413781918";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Trinité-d'Estienne d'Orves";
            station.Latitude = "48.8763290374387";
            station.Longitude = "2.33180456851059";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Rue du Bac";
            station.Latitude = "48.8558913378155";
            station.Longitude = "2.32570499389931";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Porte de la Chapelle";
            station.Latitude = "48.8979593432414";
            station.Longitude = "2.35925506957965";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Jules Joffrin";
            station.Latitude = "48.8924969769302";
            station.Longitude = "2.3443284105515";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Rennes";
            station.Latitude = "48.8481982688247";
            station.Longitude = "2.32802174862811";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Marx-Dormoy";
            station.Latitude = "48.8903740812211";
            station.Longitude = "2.35979700867061";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Corentin-Celton";
            station.Latitude = "48.8268298873941";
            station.Longitude = "2.27896686157434";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Convention";
            station.Latitude = "48.8371369495626";
            station.Longitude = "2.29639609015555";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Pernety";
            station.Latitude = "48.8339338198109";
            station.Longitude = "2.31790897216328";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Chêtillon Montrouge";
            station.Latitude = "48.8102833635107";
            station.Longitude = "2.30128887097595";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Plaisance";
            station.Latitude = "48.8317580937884";
            station.Longitude = "2.31386654984816";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Carrefour-Pleyel";
            station.Latitude = "48.9199435050834";
            station.Longitude = "2.34390941978589";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Brochant";
            station.Latitude = "48.8906266330241";
            station.Longitude = "2.32019112127532";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Gabriel-Péri";
            station.Latitude = "48.9165748359832";
            station.Longitude = "2.29432896468706";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "La Fourche";
            station.Latitude = "48.8874368425293";
            station.Longitude = "2.3257252220174";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Gaité";
            station.Latitude = "48.8387518213886";
            station.Longitude = "2.32250918622471";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Saint-Denis-Universite";
            station.Latitude = "48.9461108724873";
            station.Longitude = "2.36204519299059";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Varenne";
            station.Latitude = "48.8563930691984";
            station.Longitude = "2.31475436481028";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Malakoff-Plateau de Vanves";
            station.Latitude = "48.8224426939998";
            station.Longitude = "2.29793210532872";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Mairie de Clichy";
            station.Latitude = "48.9040054324295";
            station.Longitude = "2.30536812086686";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Mairie de Saint-Ouen";
            station.Latitude = "48.9116216803838";
            station.Longitude = "2.33373696484209";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Porte de Saint-Ouen";
            station.Latitude = "48.8975117327714";
            station.Longitude = "2.3290216472122";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Porte de Vanves";
            station.Latitude = "48.8277653265896";
            station.Longitude = "2.30431235076306";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Garibaldi";
            station.Latitude = "48.9063821642264";
            station.Longitude = "2.33191047345893";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Saint-Denis-Porte de Paris";
            station.Latitude = "48.9299266652709";
            station.Longitude = "2.35618614918886";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Basilique de Saint-Denis";
            station.Latitude = "48.9364774926504";
            station.Longitude = "2.35963974530458";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Saint-Francois-Xavier";
            station.Latitude = "48.8509825598235";
            station.Longitude = "2.31442986460027";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Porte de Clichy";
            station.Latitude = "48.8944360602979";
            station.Longitude = "2.31356566546522";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Malakoff-Rue Etienne Dolet";
            station.Latitude = "48.8147040605669";
            station.Longitude = "2.29728473017063";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Les Agnettes";
            station.Latitude = "48.922916331509";
            station.Longitude = "2.28596389657663";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Asnieres-Gennevilliers Les Courtilles";
            station.Latitude = "48.9302936228707";
            station.Longitude = "2.28376063319524";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Guy-Môquet";
            station.Latitude = "48.8930090560512";
            station.Longitude = "2.32749583949048";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Liège";
            station.Latitude = "48.8795371663808";
            station.Longitude = "2.32685794727296";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Cour Saint-Emilion";
            station.Latitude = "48.8333137155784";
            station.Longitude = "2.38729970438351";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Bibliotheque-Francois Mitterrand";
            station.Latitude = "48.8298306952711";
            station.Longitude = "2.37612000934483";
            TransportStations.Add(station);

            station = new TransportStation();
            station.Name = "Olympiades";
            station.Latitude = "48.8269482819607";
            station.Longitude = "2.36703843338759";
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
