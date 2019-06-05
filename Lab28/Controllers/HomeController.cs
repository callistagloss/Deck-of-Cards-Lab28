using Lab28.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MovieDbBreakout.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string DeckId)
        {
            string APIText = GetNewDeck(DeckId);
            Deck deck = ConvertToDeck(APIText);
            return View(deck);
        }

        public string GetAPIText(string title)
        {
            string APIkey = "62398519";
            string URL = "https://deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1";

            HttpWebRequest request = WebRequest.CreateHttp(URL);

            //There will sometimes be extra steps here between request and response

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader rd = new StreamReader(response.GetResponseStream());

            string APIText = rd.ReadToEnd();

            return APIText;
        }

        public Deck ConvertToDeck(string APIText)
        {
            JToken jsonData = JToken.Parse(APIText);
            Deck d = new Deck();
            d.DeckId = jsonData["deck_id"].ToString();

            return d;
        }

        public string GetNewDeck(string DeckId)
        {

            string shuffleDeck = "https://deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1";

            HttpWebRequest request = WebRequest.CreateHttp(shuffleDeck);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader rd = new StreamReader(response.GetResponseStream());

            string APIText = rd.ReadToEnd();

            //string deckID = APIText["deck_id"].ToString();


            return APIText;
        }

       public List<Cards> GetCardList(string InitialDraw)
            {

            InitialDraw = $"https://deckofcardsapi.com/api/deck/<<3p40paa87x90>>/draw/?count=2";

            HttpWebRequest request = WebRequest.CreateHttp(InitialDraw);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader rd = new StreamReader(response.GetResponseStream());

            string APIText = rd.ReadToEnd();

            //string deckID = APIText["deck_id"].ToString();

            JToken json = JToken.Parse(InitialDraw);
            List<JToken> cardTokens = json["InitialDraw"].ToList();

            List<Cards> CardsList = new List<Cards>();

            foreach(JToken j in cardTokens)
            {
                Cards c = new Cards();
                c.value = j["value"].ToString();
                c.suit = j["suit"].ToString();
                c.code = j["code"].ToString();
                CardsList.Add(c);
            }

            ViewBag.CardsList = CardsList;

            return CardsList;
            }

        //        public List<Movie> ConvertListOfMovies(string APIText)
        //        {
        //            JToken json = JToken.Parse(APIText);

        //            List<JToken> filmTokens = json["Search"].ToList();

        //            List<Movie> MovieResults = new List<Movie>();

        //            foreach (JToken j in filmTokens)
        //            {
        //                //Make a movie and grab out the json data
        //                Movie m = new Movie();
        //                m.Title = j["Title"].ToString();
        //                MovieResults.Add(m);
        //            }

        //            return MovieResults;
        //        }
        //        public ActionResult About()
        //        {
        //            ViewBag.Message = "Your application description page.";

        //            return View();
        //        }

        //        public ActionResult Contact()
        //        {
        //            ViewBag.Message = "Your contact page.";

        //            return View();
        //        }
        //    }
        //}
        
    } 
}
