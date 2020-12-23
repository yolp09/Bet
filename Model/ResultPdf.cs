using Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ResultPdf
    {

        public void NewSaveP1(List<Bet> bets, IEnumerable<Data.User> users, List<Match> matches)
        {
            string pathExecutiveFail = Assembly.GetExecutingAssembly().Location;
            pathExecutiveFail = pathExecutiveFail.Substring(0, pathExecutiveFail.LastIndexOf("\\")) + "\\temp\\";

            iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4);
            string name1 = "Table.pdf";
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(name1, FileMode.Create));
            doc.Open();

            string ttf = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "Arial.TTF");
            BaseFont baseFont = iTextSharp.text.pdf.BaseFont.CreateFont(ttf, iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);
            /////////////////////////////////////////////////////////////////////////
            //iTextSharp.text.Font fontU = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.UNDERLINE);
            //iTextSharp.text.Font fontMini = new iTextSharp.text.Font(baseFont, 7, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font fontTable = new iTextSharp.text.Font(baseFont, 7, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font fontTableBold = new iTextSharp.text.Font(baseFont, 9, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font fontTableBoldWhite = new iTextSharp.text.Font(baseFont, 9, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.WHITE);
            //iTextSharp.text.Font fontTableMini = new iTextSharp.text.Font(baseFont, 5, iTextSharp.text.Font.NORMAL);
            PdfPTable table;
            PdfPCell cell;
            PdfPTable tableCell;

            List<UserResult> totalResult = new List<UserResult>();
            for(int i=0;i<users.Count();i++)
            {
                totalResult.Add(new UserResult(users.ElementAt(i).Name));
            }

            string tour = bets.FirstOrDefault().Tour.TourName + " тур";

            iTextSharp.text.Paragraph txt_head = new iTextSharp.text.Paragraph(tour, font);
            txt_head.Alignment = Element.ALIGN_CENTER;
            doc.Add(txt_head);

            /// <summary>
            /// Общая таблица
            /// </summary>
            table = new PdfPTable(1 + users.Count());
            table.WidthPercentage = 100;
            table.SpacingBefore = 15;
            float[] setWidth = new float[1 + users.Count()];
            setWidth[0] = 3f;
            for (int i = 1; i < setWidth.Length; i++)
            {
                setWidth[i] = 1f;
            }
            table.SetWidths(setWidth);

            #region Заголовок с пользователями
            cell = new PdfPCell(new Phrase(new Phrase(tour, fontTableBold)));
            cell.Rowspan = 2;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.BackgroundColor = Color.LIGHT_GRAY;
            table.AddCell(cell);
            for (int i = 0; i < users.Count(); i++)
            {
                cell = new PdfPCell(new Phrase(new Phrase(users.ElementAt(i).Name, fontTableBold)));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.BackgroundColor = Color.LIGHT_GRAY;
                table.AddCell(cell);
            }
            for (int i = 0; i < users.Count(); i++)
            {
                tableCell = new PdfPTable(2);
                cell = new PdfPCell(new Phrase(new Phrase("Счет", fontTable)));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = 0;
                tableCell.AddCell(cell);
                cell = new PdfPCell(new Phrase(new Phrase("Очки", fontTable)));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = 0;
                tableCell.AddCell(cell);
                table.AddCell(tableCell);
            }
            #endregion

            iTextSharp.text.Image img;
            PdfPTable tableCellImage;
            Score score;

            for (int i = 0; i < matches.Count;i++ )
            {
                #region Запись матча
                tableCell = new PdfPTable(3);
                tableCell.SetWidths(new float[] { 2, 1, 2 });
                Color H = Color.WHITE;
                Color A = Color.WHITE;
                if (matches[i].ScoreH > matches[i].ScoreA)
                {
                    H = Color.GREEN;
                }
                else if (matches[i].ScoreH < matches[i].ScoreA)
                {
                    A = Color.GREEN;
                }
                else if (matches[i].ScoreH == matches[i].ScoreA && matches[i].ScoreH != null)
                {
                    H = Color.YELLOW;
                    A = Color.YELLOW;
                }
                img = Image.GetInstance(pathExecutiveFail + matches[i].CommandH.Logo.Name);
                img.ScaleAbsolute(10, 10);
                tableCellImage = new PdfPTable(2);
                tableCellImage.SetWidths(new float[] { 1, 2 });

                cell = new PdfPCell(img);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.BackgroundColor = H;
                cell.Border = 0;
                tableCellImage.AddCell(cell);

                cell = new PdfPCell(new Phrase(new Phrase(matches[i].CommandH.Name, fontTable)));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.BackgroundColor = H;
                cell.Border = 0;
                tableCellImage.AddCell(cell);

                tableCell.AddCell(tableCellImage);


                cell = new PdfPCell(new Phrase(new Phrase(matches[i].ScoreH + " : " + matches[i].ScoreA, fontTableBold)));

                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                tableCell.AddCell(cell);

                img = Image.GetInstance(pathExecutiveFail + matches[i].CommandA.Logo.Name);
                img.ScaleAbsolute(10, 10);
                tableCellImage = new PdfPTable(2);
                tableCellImage.SetWidths(new float[] { 1, 2 });

                cell = new PdfPCell(img);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.BackgroundColor = A;
                cell.Border = 0;
                tableCellImage.AddCell(cell);

                cell = new PdfPCell(new Phrase(new Phrase(matches[i].CommandA.Name, fontTable)));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.BackgroundColor = A;
                cell.Border = 0;
                tableCellImage.AddCell(cell);

                tableCell.AddCell(tableCellImage);
                table.AddCell(tableCell);
                #endregion

                #region Запись Bet
                for (int q = 0; q < users.Count(); q++)
                {
                    for (int j = 0; j < bets.Count; j++)
                    {
                        if(bets[j].User == users.ElementAt(q) && bets[j].Match == matches[i])
                        {
                            tableCell = new PdfPTable(2);
                            cell = new PdfPCell(new Phrase(new Phrase(bets[j].ScoreH + " : " + bets[j].ScoreA, fontTableBold)));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            tableCell.AddCell(cell);

                            score = new Score(bets[j].Match.ScoreH, bets[j].Match.ScoreA, bets[j].ScoreH, bets[j].ScoreA);

                            cell = new PdfPCell(new Phrase(new Phrase(score.ScoreResult().ToString(), score.FontCell)));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell.BackgroundColor = score.ColorCell;
                            tableCell.AddCell(cell);
                            table.AddCell(tableCell);

                            totalResult[q].CountGame += score.Result.CountGame;
                            totalResult[q].TC += score.Result.TC;
                            totalResult[q].IIPM += score.Result.IIPM;
                            totalResult[q].IIZM += score.Result.IIZM;
                            totalResult[q].II += score.Result.II;
                            totalResult[q].ZM += score.Result.ZM;
                            totalResult[q].P += score.Result.P;
                            totalResult[q].Score += score.Result.Score;
                        }
                    }
                }
                #endregion
            }

            #region Запись итогов
            cell = new PdfPCell(new Phrase(new Phrase("Итого:", fontTableBold)));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            table.AddCell(cell);
            for (int i = 0; i < users.Count();i++ )
            {
                cell = new PdfPCell(new Phrase(new Phrase(totalResult[i].Score.ToString(), fontTableBold)));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(cell);
            }
            #endregion

            doc.Add(table);

            txt_head = new iTextSharp.text.Paragraph("Итоговая таблица", font);
            txt_head.Alignment = Element.ALIGN_CENTER;
            txt_head.SpacingBefore = 30;
            doc.Add(txt_head);

            #region Итоговая таблица
            table = new PdfPTable(10);
            table.WidthPercentage = 100;
            table.SpacingBefore = 15;

            cell = new PdfPCell(new Phrase(new Phrase("Место", fontTableBold)));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.BackgroundColor = Color.LIGHT_GRAY;
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Phrase("Участники", fontTableBold)));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.BackgroundColor = Color.LIGHT_GRAY;
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Phrase("И", fontTableBold)));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.BackgroundColor = Color.LIGHT_GRAY;
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Phrase("ТС\r\n(Х7)", fontTableBold)));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.BackgroundColor = Color.LIGHT_GRAY;
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Phrase("ИИ+РМ\r\n(Х5)", fontTableBold)));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.BackgroundColor = Color.LIGHT_GRAY;
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Phrase("ИИ+ЗМ\r\n(Х4)", fontTableBold)));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.BackgroundColor = Color.LIGHT_GRAY;
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Phrase("ИИ\r\n(Х3)", fontTableBold)));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.BackgroundColor = Color.LIGHT_GRAY;
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Phrase("ЗМ\r\n(Х1)", fontTableBold)));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.BackgroundColor = Color.LIGHT_GRAY;
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Phrase("П", fontTableBold)));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.BackgroundColor = Color.LIGHT_GRAY;
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Phrase("Очки", fontTableBold)));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.BackgroundColor = Color.LIGHT_GRAY;
            table.AddCell(cell);

            var sortTotalResult = totalResult.OrderByDescending(ur => ur.Score).ThenByDescending(ur => ur.TC).ThenByDescending(ur => ur.IIPM).ThenByDescending(ur => ur.IIZM).ThenByDescending(ur => ur.II).ThenByDescending(ur => ur.ZM);
            for (int i = 0; i < sortTotalResult.Count(); i++)
            {
                cell = new PdfPCell(new Phrase(new Phrase((i+1).ToString(), fontTableBold)));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.BackgroundColor = Color.ORANGE;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(new Phrase(sortTotalResult.ElementAt(i).NameUser, fontTableBold)));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(new Phrase(sortTotalResult.ElementAt(i).CountGame.ToString(), fontTableBold)));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(new Phrase(sortTotalResult.ElementAt(i).TC.ToString(), fontTableBold)));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(new Phrase(sortTotalResult.ElementAt(i).IIPM.ToString(), fontTableBold)));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(new Phrase(sortTotalResult.ElementAt(i).IIZM.ToString(), fontTableBold)));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(new Phrase(sortTotalResult.ElementAt(i).II.ToString(), fontTableBold)));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(new Phrase(sortTotalResult.ElementAt(i).ZM.ToString(), fontTableBold)));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(new Phrase(sortTotalResult.ElementAt(i).P.ToString(), fontTableBold)));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(new Phrase(sortTotalResult.ElementAt(i).Score.ToString(), fontTableBold)));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(cell);
            }
            #endregion

                doc.Add(table);

            doc.Close();

            if (File.Exists(name1))
            {
                Process.Start(name1);
            }
        }

        private class Score
        {
            private Font fontTableBold;
            private Font fontTableBoldWhite;
            public Score(int? scoreH, int? scoreA, int? betH, int? betA)
            {
                ResultDifference = scoreH - scoreA;
                ResultGame = ResultDifference == 0 ? ResultPdf.ResultGame.Draw : ResultDifference > 0 ? ResultPdf.ResultGame.WinH : ResultPdf.ResultGame.WinA;

                BetDifference = betH - betA;
                BetGame = BetDifference == 0 ? ResultPdf.ResultGame.Draw : BetDifference > 0 ? ResultPdf.ResultGame.WinH : ResultPdf.ResultGame.WinA;

                CountH = (scoreH == betH) ? true : false;
                CountA = (scoreA == betA) ? true : false;

                Null = (scoreH == null || scoreA == null || betH == null || betA == null) ? true : false;

                Result = new UserResult(null);

                ColorCell = Color.WHITE;

                string ttf = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "Arial.TTF");
                BaseFont baseFont = iTextSharp.text.pdf.BaseFont.CreateFont(ttf, iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);
                fontTableBold = new iTextSharp.text.Font(baseFont, 9, iTextSharp.text.Font.BOLD);
                fontTableBoldWhite = new iTextSharp.text.Font(baseFont, 9, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.WHITE);

                FontCell = fontTableBold;

            }
            public ResultGame ResultGame { get; private set; }
            public ResultGame BetGame { get; private set; }
            public int? ResultDifference { get; private set; }
            public int? BetDifference { get; private set; }
            public bool CountH { get; private set; }
            public bool CountA { get; private set; }
            public bool Null { get; private set; }
            public UserResult Result { get; private set; }
            public Color ColorCell { get; private set; }
            public Font FontCell { get; private set; }

            public int ScoreResult()
            {
                if (Null)
                    return 0;
                if (ResultGame == BetGame && CountH && CountA)
                {
                    Result.CountGame++;
                    Result.TC++;
                    Result.Score += 7;
                    ColorCell = Color.RED;
                    FontCell = fontTableBoldWhite;
                    return 7;
                }
                else if (ResultGame == BetGame && ResultDifference == BetDifference)
                {
                    Result.CountGame++;
                    Result.IIPM++;
                    Result.Score += 5;
                    ColorCell = Color.BLUE;
                    FontCell = fontTableBoldWhite;
                    return 5;
                }
                else if (ResultGame == BetGame && (CountH || CountA))
                {
                    Result.CountGame++;
                    Result.IIZM++;
                    Result.Score += 4;
                    ColorCell = Color.ORANGE;
                    return 4;
                }
                else if (ResultGame == BetGame)
                {
                    Result.CountGame++;
                    Result.II++;
                    Result.Score += 3;
                    ColorCell = Color.YELLOW;
                    return 3;
                }
                else if (CountH || CountA)
                {
                    Result.CountGame++;
                    Result.ZM++;
                    Result.Score += 1;
                    ColorCell = Color.DARK_GRAY;
                    FontCell = fontTableBoldWhite;
                    return 1;
                }

                Result.CountGame++;
                Result.P++;
                return 0;
            }
        }

        private class UserResult
        {
            public UserResult(string nameUser)
            {
                NameUser = nameUser;
            }
            public int CountGame { get; set; }
            public int TC { get; set; } //7
            public int IIPM { get; set; } //5
            public int IIZM { get; set; } //4
            public int II { get; set; } //3
            public int ZM { get; set; } //1
            public int P { get; set; } // 0
            public int Score { get; set; }
            public string NameUser { get; private set; }
        }

        private enum ResultGame
        {
            WinH,
            WinA,
            Draw
        }
    }
}
