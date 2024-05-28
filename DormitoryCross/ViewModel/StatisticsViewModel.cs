using DormitoryCross.Services;
using DormitoryCross.View;
using Microsoft.AspNetCore.Server.Kestrel.Internal.System.Collections.Sequences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryCross.ViewModel
{
    public partial class StatisticsViewModel : BaseViewModel
    {
        SQLServices sQLServices;

        public ObservableCollection<Room> Rooms { get; } = new();
        public ObservableCollection<Student> Students { get; } = new();
        List<String> filledRooms = new List<string>();
        List<String> freeRooms = new List<string>();

        [ObservableProperty]
        int leftStudents = 0;

        [ObservableProperty]
        int rigthStudents = 0;

        public List<string> leftRooms = new List<string>
        {
           "1A",    "2A",   "3A",   "4A",   "5A",   "6A",   "7A",   "8A",   "4",    "5",    "6",    "7",    "8",    "9",    "10",   "11",   "12",
            "13",   "14",   "15",   "16",   "17",   "18",   "19",   "20",   "21",   "22",   "23",   "24",   "25",   "26",   "27",   "28",   "29",   "30",   "31",   "32",
            "33",   "34",   "35",   "36",   "37",   "38",   "39",   "40",   "41",   "42",   "43",   "44",   "45",   "46",   "47",   "48",   "49",   "50",   "51",   "52",
            "53",   "54",   "55",   "56",   "57",   "58",   "59",   "60",   "61",   "62",   "63",   "64",   "65",   "66",   "67",   "68",   "69",   "70",   "71",   "72",
            "73",   "74",   "75",   "76",   "77",   "78",   "79",   "80",   "81",   "82",   "83",   "84",   "85",   "86",   "87",   "88",   "89",   "90",   "91",   "92",
            "93",   "94",   "95",   "96",   "97",   "98",   "99",   "100",  "101",  "102",  "103",  "104",  "105",  "106",  "107",  "108",  "109",  "110",  "111",  "112",
            "113",  "114",  "115",  "116",  "117",  "118",  "119"
        };

        public List<string> rigthRooms = new List<string>
        {
            "120",  "121",  "122",  "123",  "124",  "125",  "126",  "127",  "128",  "129",  "130",  "131",  "132",
            "133",  "134",  "135",  "136",  "137",  "138",  "139",  "140",  "141",  "142",  "143",  "144",  "145",  "146",  "147",  "148",  "149",  "150",  "151",  "152",
            "153",  "154",  "155",  "156",  "157",  "158",  "159",  "160",  "161",  "162",  "163",  "164",  "165",  "166",  "167",  "168",  "169",  "170",  "171",  "172",
            "173",  "174",  "175",  "176",  "177",  "178",  "179",  "180",  "181",  "182",  "183",  "184",  "185",  "186",  "187",  "188",  "189",  "190",  "191",  "192",
            "193",  "194",  "195",  "196",  "197",  "198",  "199",  "200",  "201",  "202",  "203",  "204",  "205",  "206",  "207",  "208",  "209",  "210",  "211",  "212",
            "213",  "214",  "215",  "216",  "217",  "218",  "219",  "220",  "221",  "222",  "223",  "224",  "225",  "226",  "227",  "228",  "229",  "230",  "231",  "232",
            "233",  "234",  "235",  "236",  "237",  "238",  "239",  "240",  "241",  "242",  "243",  "244",  "245",  "246",  "247"
        };

        public List<string> allRooms = new List<string>
        {
           "1A",    "2A",   "3A",   "4A",   "5A",   "6A",   "7A",   "8A",   "4",    "5",    "6",    "7",    "8",    "9",    "10",   "11",   "12",
            "13",   "14",   "15",   "16",   "17",   "18",   "19",   "20",   "21",   "22",   "23",   "24",   "25",   "26",   "27",   "28",   "29",   "30",   "31",   "32",
            "33",   "34",   "35",   "36",   "37",   "38",   "39",   "40",   "41",   "42",   "43",   "44",   "45",   "46",   "47",   "48",   "49",   "50",   "51",   "52",
            "53",   "54",   "55",   "56",   "57",   "58",   "59",   "60",   "61",   "62",   "63",   "64",   "65",   "66",   "67",   "68",   "69",   "70",   "71",   "72",
            "73",   "74",   "75",   "76",   "77",   "78",   "79",   "80",   "81",   "82",   "83",   "84",   "85",   "86",   "87",   "88",   "89",   "90",   "91",   "92",
            "93",   "94",   "95",   "96",   "97",   "98",   "99",   "100",  "101",  "102",  "103",  "104",  "105",  "106",  "107",  "108",  "109",  "110",  "111",  "112",
            "113",  "114",  "115",  "116",  "117",  "118",  "119",  "120",  "121",  "122",  "123",  "124",  "125",  "126",  "127",  "128",  "129",  "130",  "131",  "132",
            "133",  "134",  "135",  "136",  "137",  "138",  "139",  "140",  "141",  "142",  "143",  "144",  "145",  "146",  "147",  "148",  "149",  "150",  "151",  "152",
            "153",  "154",  "155",  "156",  "157",  "158",  "159",  "160",  "161",  "162",  "163",  "164",  "165",  "166",  "167",  "168",  "169",  "170",  "171",  "172",
            "173",  "174",  "175",  "176",  "177",  "178",  "179",  "180",  "181",  "182",  "183",  "184",  "185",  "186",  "187",  "188",  "189",  "190",  "191",  "192",
            "193",  "194",  "195",  "196",  "197",  "198",  "199",  "200",  "201",  "202",  "203",  "204",  "205",  "206",  "207",  "208",  "209",  "210",  "211",  "212",
            "213",  "214",  "215",  "216",  "217",  "218",  "219",  "220",  "221",  "222",  "223",  "224",  "225",  "226",  "227",  "228",  "229",  "230",  "231",  "232",
            "233",  "234",  "235",  "236",  "237",  "238",  "239",  "240",  "241",  "242",  "243",  "244",  "245",  "246",  "247"
        };

        [ObservableProperty]
        bool isRefreshing;

        public StatisticsViewModel()
        {
            Title = "Статистика";

            this.sQLServices = new SQLServices();

            Refresh();
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

                Students.Clear();

                var students = await sQLServices.GetStudent();
                string n = "";

                foreach (var student in students)
                {
                    Students.Add(student);
                    if (allRooms.Contains(student.NumberRoom))
                    {
                        filledRooms.Add(student.NumberRoom);
                    }

                    if (leftRooms.Contains(student.NumberRoom))
                    {
                        LeftStudents++;
                    }
                    else if (rigthRooms.Contains(student.NumberRoom))
                    {
                        RigthStudents++;
                    }
                }

                foreach(var r in allRooms)
                {
                   if (!filledRooms.Contains(r))
                   {
                        Rooms.Add(new Room(r));
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
            }
        }

        [RelayCommand]
        async Task GoToRoomPageAsync(Room room)
        {
            if (room is null)
                return;

            await Shell.Current.GoToAsync($"{nameof(RoomPage)}?NumberRoom={room.Number}");
            isRefreshing = true;
        }
    }
}
