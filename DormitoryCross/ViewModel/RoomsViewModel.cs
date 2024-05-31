using DormitoryCross.Services;
using DormitoryCross.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryCross.ViewModel
{
    public partial class RoomsViewModel : BaseViewModel
    {
        SQLServices sQLServices;

        public ObservableCollection<Room> Rooms { get; } = new();

        [ObservableProperty]
        bool isRefreshing;

        public List<string> threeRooms = new List<string>
        {
            "1A", "8", "16", "24", "32", "40", "48", "56", "64", "72", "80", "88", "96", "104",
            "112", "120", "128", "136", "144", "152", "160", "168", "176", "184", "192", "200", "208", "216", "224", "232", "240",
            "4A", "11", "19", "27", "35", "43", "51", "59", "67", "75", "83", "91", "99", "107", "115", "123", "131", "139", "147",
            "155", "163", "171", "179", "187", "195", "203", "211", "219", "227", "235", "243", "5A", "4", "12", "20", "28", "36",
            "44", "52", "60", "68", "76", "84", "92", "100", "108", "116", "124", "132", "140", "148", "156", "164", "172", "180",
            "188", "196", "204", "212", "220", "228", "236", "244", "8A", "7", "15", "23", "31", "39", "47", "55", "63", "71", "79",
            "87", "95", "103", "111", "119", "127", "135", "143", "151", "159", "167", "175", "183", "191", "199", "207", "215",
            "223","231", "239", "247"
        };

        public List<string> twoRooms = new List<string>
        {
            "2A",   "9",    "17",   "25",   "33",   "41",   "49",   "57",   "65",   "73",   "81",
            "89",   "97",   "105",  "113",  "121",  "129",  "137",  "145",  "153",  "161",  "169",  "177",  "185",  "193",  "201",
            "209",  "217",  "225",  "233",  "241",  "3A",   "10",   "18",   "26",   "34",   "42",   "50",   "58",   "66",   "74",
            "82",   "90",   "98",   "106",  "114",  "122",  "130",  "138",  "146",  "154",  "162",  "170",  "178",  "186",  "194",
            "202",  "210",  "218",  "226",  "234",  "242",  "6A",   "5",    "13",   "21",   "29",   "37",   "45",   "53",   "61",
            "69",   "77",   "85",   "93",   "101",  "109",  "117",  "125",  "133",  "141",  "149",  "157",  "165",  "173",  "181",
            "189",  "197",  "205",  "213",  "221",  "229",  "237",  "245",  "7A",   "6",    "14",   "22",   "30",   "38",   "46",
            "54",   "62",   "70",   "78",   "86",   "94",   "102",  "110",  "118",  "126",  "134",  "142",  "150",  "158",  "166",
            "174",  "182",  "190",  "198",  "206",  "214",  "222",  "230",  "238",  "246"
        };

        public Dictionary<string, int> mapRooms = new Dictionary<string, int>
        {
            {"1A", 0},  {"2A", 0},  {"3A", 0},  {"4A", 0},  {"5A", 0},  {"6A", 0},  {"7A", 0},  {"8A", 0},  {"4", 0},   {"5", 0},   {"6", 0},   {"7", 0},   {"8", 0},   {"9", 0}, {"10", 0}, {"11", 0}, {"12", 0},
            {"13", 0},  {"14", 0},  {"15", 0},  {"16", 0},  {"17", 0},  {"18", 0},  {"19", 0},  {"20", 0},  {"21", 0},  {"22", 0},  {"23", 0},  {"24", 0},  {"25", 0},  {"26", 0}, {"27", 0}, {"28", 0}, {"29", 0}, {"30", 0}, {"31", 0}, {"32", 0},
            {"33", 0},  {"34", 0},  {"35", 0},  {"36", 0},  {"37", 0},  {"38", 0},  {"39", 0},  {"40", 0},  {"41", 0},  {"42", 0},  {"43", 0},  {"44", 0},  {"45", 0},  {"46", 0}, {"47", 0}, {"48", 0}, {"49", 0}, {"50", 0}, {"51", 0}, {"52", 0},
            {"53", 0},  {"54", 0},  {"55", 0},  {"56", 0},  {"57", 0},  {"58", 0},  {"59", 0},  {"60", 0},  {"61", 0},  {"62", 0},  {"63", 0},  {"64", 0},  {"65", 0},  {"66", 0}, {"67", 0}, {"68", 0}, {"69", 0}, {"70", 0}, {"71", 0}, {"72", 0},
            {"73", 0},  {"74", 0},  {"75", 0},  {"76", 0},  {"77", 0},  {"78", 0},  {"79", 0},  {"80", 0},  {"81", 0},  {"82", 0},  {"83", 0},  {"84", 0},  {"85", 0},  {"86", 0}, {"87", 0}, {"88", 0}, {"89", 0}, {"90", 0}, {"91", 0}, {"92", 0},
            {"93", 0},  { "94", 0}, {"95", 0},  {"96", 0},  {"97", 0},  {"98", 0},  {"99", 0},  {"100", 0}, {"101", 0}, {"102", 0}, {"103", 0}, {"104", 0}, {"105", 0}, {"106", 0}, {"107", 0}, {"108", 0}, {"109", 0}, {"110", 0}, {"111", 0}, {"112", 0},
            {"113", 0}, {"114", 0}, {"115", 0}, {"116", 0}, {"117", 0}, {"118", 0}, {"119", 0}, {"120", 0}, {"121", 0}, {"122", 0}, {"123", 0}, {"124", 0}, {"125", 0}, {"126", 0}, {"127", 0}, {"128", 0}, {"129", 0}, {"130", 0}, {"131", 0}, {"132", 0},
            {"133", 0}, {"134", 0}, {"135", 0}, {"136", 0}, {"137", 0}, {"138", 0}, {"139", 0}, {"140", 0}, {"141", 0}, {"142", 0}, {"143", 0}, {"144", 0}, {"145", 0}, {"146", 0}, {"147", 0}, {"148", 0}, {"149", 0}, {"150", 0}, {"151", 0}, {"152", 0},
            {"153", 0}, {"154", 0}, {"155", 0}, {"156", 0}, {"157", 0}, {"158", 0}, {"159", 0}, {"160", 0}, {"161", 0}, {"162", 0}, {"163", 0}, {"164", 0}, {"165", 0}, {"166", 0}, {"167", 0}, {"168", 0}, {"169", 0}, {"170", 0}, {"171", 0}, {"172", 0},
            {"173", 0}, {"174", 0}, {"175", 0}, {"176", 0}, {"177", 0}, {"178", 0}, {"179", 0}, {"180", 0}, {"181", 0}, {"182", 0}, {"183", 0}, {"184", 0}, {"185", 0}, {"186", 0}, {"187", 0}, {"188", 0}, {"189", 0}, {"190", 0}, {"191", 0}, {"192", 0},
            {"193", 0}, {"194", 0}, {"195", 0}, {"196", 0}, {"197", 0}, {"198", 0}, {"199", 0}, {"200", 0}, {"201", 0}, {"202", 0}, {"203", 0}, {"204", 0}, {"205", 0}, {"206", 0}, {"207", 0}, {"208", 0}, {"209", 0}, {"210", 0}, {"211", 0}, {"212", 0},
            {"213", 0}, {"214", 0}, {"215", 0}, {"216", 0}, {"217", 0}, {"218", 0}, {"219", 0}, {"220", 0}, {"221", 0}, {"222", 0}, {"223", 0}, {"224", 0}, {"225", 0}, {"226", 0}, {"227", 0}, {"228", 0}, {"229", 0}, {"230", 0}, {"231", 0}, {"232", 0},
            {"233", 0}, {"234", 0}, {"235", 0}, {"236", 0}, {"237", 0}, {"238", 0}, {"239", 0}, {"240", 0}, {"241", 0}, {"242", 0}, {"243", 0}, {"244", 0}, {"245", 0}, {"246", 0}, {"247", 0}
    };

        public RoomsViewModel(SQLServices sQLServices)
        {
            Title = "Комнаты";
            this.sQLServices = new SQLServices();
            CreateButton();
            OnPropertyChanged();
        }

        public async Task CreateButton()
        {
            Rooms.Clear();

            Rooms.Add(new Room("1A", Colors.Green));
            Rooms.Add(new Room("2A", Colors.Green));
            Rooms.Add(new Room("3A", Colors.Green));
            Rooms.Add(new Room("4A", Colors.Green));
            Rooms.Add(new Room("5A", Colors.Green));
            Rooms.Add(new Room("6A", Colors.Green));
            Rooms.Add(new Room("7A", Colors.Green));
            Rooms.Add(new Room("8A", Colors.Green));

            for (int i = 4; i < 248; i++)
            {
                Rooms.Add(new Room($"{i}", Colors.Green));
            }

            await Refresh();
        }

        [RelayCommand]
        async Task Refresh()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                await Task.Delay(2000);

                var students = await sQLServices.GetStudent();

                foreach (var student in students)
                {
                    var number = student.NumberRoom;
                    if (mapRooms.ContainsKey(number))
                    {
                        mapRooms[number]++;

                        if (threeRooms.Contains(number))
                        {
                            if (mapRooms[number] > 3)
                            {
                                foreach (var room in Rooms)
                                {
                                    if (room.Number.Equals(number))
                                        room.Color = Colors.Red;
                                    OnPropertyChanged();
                                }
                            }
                            else if (mapRooms[number].Equals(3))
                            {
                                foreach (var room in Rooms)
                                {
                                    if (room.Number.Equals(number))
                                        room.Color = Color.FromHex("#527da3");
                                    OnPropertyChanged();
                                }
                            }
                        }
                        else if (twoRooms.Contains(number))
                        {
                            if (mapRooms[number] > 2)
                            {
                                foreach (var room in Rooms)
                                {
                                    if (room.Number.Equals(number))
                                        room.Color = Colors.Red;
                                    OnPropertyChanged();
                                }
                            }
                            else if (mapRooms[number].Equals(2))
                            {
                                foreach (var room in Rooms)
                                {
                                    if (room.Number.Equals(number))
                                        room.Color = Color.FromHex("#527da3");
                                    OnPropertyChanged();
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error", $"get study: {ex.Message}", "Ok");

            }
            finally
            {
                IsBusy = false;
                isRefreshing = false;
                OnPropertyChanged();
            }
        }

        [RelayCommand]
        async Task GoToRoomPageAsync(Room room)
        {
            if (room is null)
                return;

            await Shell.Current.GoToAsync($"{nameof(RoomPage)}?NumberRoom={room.Number}");
        }
    }
}
