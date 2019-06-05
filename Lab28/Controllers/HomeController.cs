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
            public Deck GetDeck()
            {

                string url = $"https://deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1";

                HttpWebRequest request = WebRequest.CreateHttp(url);
             
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader rd = new StreamReader(response.GetResponseStream());

                string APIText = rd.ReadToEnd();
                Deck deck = ConvertToDeck(APIText);
                return deck;
            }

            public Deck ConvertToDeck(string APIText)
            {
                JToken t = JToken.Parse(APIText);
                Deck deck = new Deck();
                deck.DeckId = t["deck_id"].ToString();

                Session["Deck"] = deck;
                return deck;
            }

            public List<Cards> GetCards(Deck deck, int count = 5)
            {
                List<Cards> cards = new List<Cards>();

                string url = $"https://deckofcardsapi.com/api/deck/{deck.DeckId}/draw/?count={count}";

                HttpWebRequest request = WebRequest.CreateHttp(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader rd = new StreamReader(response.GetResponseStream());
                string APIText = rd.ReadToEnd();

                JToken t = JToken.Parse(APIText);
      
                Session["Deck"] = deck;
                List<JToken> jtokens = t["cards"].ToList();
                foreach (JToken jtoken in jtokens)
                {
                    Cards card = ConvertToCard(jtoken);
                    cards.Add(card);
                }

                return cards;
            }

            public Cards ConvertToCard(JToken jToken)
            {

                Cards card = new Cards
                {
                    Image = jToken["image"].ToString(),
                    Value = jToken["value"].ToString(),
                    Suit = jToken["suit"].ToString(),
                    Code = jToken["code"].ToString()
                };

                return card;
            }

            public ActionResult Index()
            {
                Deck deck = GetDeck();
                List<Cards> cards = GetCards(deck);
                Session["CardsInHand"] = cards;

                return View(cards);
            }

            [HttpPost]
            public ActionResult Index(int count, string Card1, string Card2, string Card3, string Card4, string Card5)
            {
                Deck deck = (Deck)Session["Deck"];
                List<string> newCardList = new List<string>() { Card1, Card2, Card3, Card4, Card5 };
                
                return View();
            
            }
        }
    }