using Sports_Exercise_Battle.SEB;

namespace Sports_Exercise_Battle_UnitTests
{
    public class Tests
    {
        [Test]
        public void TournamentPlacementTest()
        {
            // test if tournament concludes with person 1 winning

            // setup
            List<UserHistory> histories = new List<UserHistory>();
            List<TournamentEntry> entries = new List<TournamentEntry>();

            for (int i = 0; i < 2; i++)
            {
                histories.Add(new UserHistory());
                histories[i].Name = "pushup";
                histories[i].DurationInSeconds = 1;
                histories[i].Count = 10+i;

                entries.Add(new TournamentEntry(histories[i], "person " + i));
            }
            Tournament testTournament = new Tournament(1);
            foreach (TournamentEntry entry in entries)
            {
                testTournament.AddEntry(entry, 1);
            }

            Assert.That(testTournament.FirstPlace == "person 1");
        }

        [Test]
        public void TournamentMultipleEntryTest()
        {
            // test if multiple consectutive entries allows person 0 to catch up again

            // setup (same as tournamentplacementtest
            List<UserHistory> histories = new List<UserHistory>();
            List<TournamentEntry> entries = new List<TournamentEntry>();

            for (int i = 0; i < 2; i++)
            {
                histories.Add(new UserHistory());
                histories[i].Name = "pushup";
                histories[i].DurationInSeconds = 1;
                histories[i].Count = 10 + i;

                entries.Add(new TournamentEntry(histories[i], "person " + i));
            }

            // add another entry to person 0 to catch up to firstplace again
            entries.Add(new TournamentEntry(histories[0], "person 0"));

            Tournament testTournament = new Tournament(1);
            foreach (TournamentEntry entry in entries)
            {
                testTournament.AddEntry(entry, 1);
            }

            Assert.That(testTournament.FirstPlace == "person 0");
        }
        [Test]
        public void TournamentManagerAccessibleTest()
        {
            // check if TournamentManager Singleton is accessible
            BLL_TournamentManager TournamentManager = BLL_TournamentManager.Instance;
            Assert.That(TournamentManager != null);
        }
        [Test]
        public void TournamentManagerNewTournament()
        {
            // check if adding entry creates a new tournament
            // get singleton instance
            BLL_TournamentManager TournamentManager = BLL_TournamentManager.Instance;

            Assert.That(TournamentManager.GetLatestTournament() == null);

            // add entry
            TournamentManager.NewTournamentEntry(new TournamentEntry(new UserHistory(), "person"), 1);

            Assert.That(TournamentManager.GetLatestTournament() != null);
        }


        [Test]
        public void SessionManagerAccessibleTest()
        {
            // check if SessionManager Singleton is accessible
            BLL_SessionManager SessionManager = BLL_SessionManager.Instance;
            Assert.That(SessionManager != null);
        }

        [Test]
        public void SessionManagerCreateSessionTest()
        {
            // check if SessionManager correctly stores all information
            BLL_SessionManager SessionManager = BLL_SessionManager.Instance;

            SessionManager.NewSession(1, "person", "token", "PersonProfileName", 1);

            Assert.That(SessionManager.OpenSessions[0].UserID == 1);
            Assert.That(SessionManager.OpenSessions[0].Username == "person");
            Assert.That(SessionManager.OpenSessions[0].UserToken == "token");
            Assert.That(SessionManager.OpenSessions[0].Elo == 1);
        }

        [Test]
        public void SessionManagerUpdateSessionTest()
        {
            // check if SessionManager correctly updates information
            BLL_SessionManager SessionManager = BLL_SessionManager.Instance;

            SessionManager.NewSession(1, "person", "token", "PersonProfileName", 1);

            Assert.That(SessionManager.OpenSessions[0].ProfileName == "PersonProfileName");
            SessionManager.UpdateSession("person", "PersonName");
            Assert.That(SessionManager.OpenSessions[0].ProfileName == "PersonName");
        }

        [Test]
        public void SessionManagerGetElo()
        {
            // check if SessionManager correctly returns ELO
            BLL_SessionManager SessionManager = BLL_SessionManager.Instance;

            SessionManager.NewSession(1, "person", "token", "PersonProfileName", 1);

            Assert.That(SessionManager.GetELO(1) == 1);
        }

        [Test]
        public void SessionManagerUpdateElo()
        {
            // check if SessionManager correctly updates ELO
            BLL_SessionManager SessionManager = BLL_SessionManager.Instance;

            SessionManager.NewSession(1, "person", "token", "PersonProfileName", 1);

            Assert.That(SessionManager.GetELO(1) == 1);

            SessionManager.UpdateElo("person", 5);

            Assert.That(SessionManager.GetELO(1) == 6);
        }

        [Test]
        public void SessionManagerIsolateToken()
        {
            // check if SessionManager correctly updates ELO
            BLL_SessionManager SessionManager = BLL_SessionManager.Instance;

            SessionManager.NewSession(1, "person", "Basic: token", "PersonProfileName", 1);

            Assert.That(SessionManager.OpenSessions[0].UserToken == "token");
        }
    }
}