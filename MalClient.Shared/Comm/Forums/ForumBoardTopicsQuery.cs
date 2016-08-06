﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MalClient.Shared.Models.Forums;
using MalClient.Shared.Utils.Enums;

namespace MalClient.Shared.Comm.Forums
{
    public class ForumBoardTopicsQuery : Query
    {
        private static Dictionary<ForumBoards, Dictionary<int, List<ForumTopicEntry>>> _boardCache =
            new Dictionary<ForumBoards, Dictionary<int, List<ForumTopicEntry>>>();

        private ForumBoards _board;
        private int _page;
        
        public ForumBoardTopicsQuery(ForumBoards board,int page)
        {
            Request =
                WebRequest.Create(Uri.EscapeUriString($"http://myanimelist.net/forum/{GetEndpoint(board)}"));
            Request.ContentType = "application/x-www-form-urlencoded";
            Request.Method = "GET";
            _board = board;
            _page = page;
        }

        

        public async Task<List<ForumTopicEntry>> GetTopicPosts()
        {
            if (_boardCache.ContainsKey(_board) && _boardCache[_board].ContainsKey(_page))
                return _boardCache[_board][_page];

            var output = new List<ForumTopicEntry>();
            var raw = await GetRequestResponse();
            if (string.IsNullOrEmpty(raw))
                return null;
            var doc = new HtmlDocument();
            doc.LoadHtml(raw);

            var topicContainer =
                doc.DocumentNode.Descendants("table")
                    .First(node => node.Attributes.Contains("id") && node.Attributes["id"].Value == "forumTopics");
            foreach (var topicRow in topicContainer.Descendants("tr").Skip(1)) //skip forum table header
            {
                try
                {
                    var current = new ForumTopicEntry();
                    var tds = topicRow.Descendants("td").ToList();

                    current.Type = tds[1].ChildNodes[0].InnerText;

                    var titleLinks = tds[1].Descendants("a").ToList();
                    var titleLink = titleLinks[0].InnerText.Length == 0 ? titleLinks[1] : titleLinks[0];

                    current.Title = titleLink.InnerText;
                    current.Url = "http://myanimelist.net" + titleLink.Attributes["href"].Value;
                    current.Id = titleLink.Attributes["href"].Value.Split('=').Last();

                    var spans = tds[1].Descendants("span").ToList();
                    current.Op = spans[0].InnerText;
                    current.Created = spans[1].InnerText;

                    current.Replies = tds[2].InnerText;

                    current.LastPoster = tds[3].Descendants("a").First().InnerText;
                    current.LastPostDate = tds[3].ChildNodes.Last().InnerText;

                    output.Add(current);
                }
                catch (Exception)
                {
                    //hatml
                }

            }

            if(!_boardCache.ContainsKey(_board))
                _boardCache[_board] = new Dictionary<int, List<ForumTopicEntry>>();
            _boardCache[_board].Add(_page, output);

            return output;
        }

        private static string GetEndpoint(ForumBoards board)
        {
            if (board == ForumBoards.AnimeSeriesDisc || board == ForumBoards.MangaSeriesDisc)
                return $"?subboard={(int) board - 100}"; //100 is offset to differentiate from other boards
            return $"?board={(int) board}";
        }
    }
}