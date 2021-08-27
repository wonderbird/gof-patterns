using Xunit;

namespace Bridge.Tests
{
    public class ProgramTests
    {
        [Fact]
        public void Main_HappyPathSyncLogger_LogsTwoMessages()
        {
            Program.Logger = new SyncLogger();
            Program.Main(null);

            var allMessagesCount = Program.Logger.GetAllMessages().Count;
            Assert.Equal(2, allMessagesCount);
        }

        [Fact]
        public void Main_HappyPathAsyncLogger_LogsTwoMessages()
        {
            Program.Logger = new AsyncLogger();

            Program.Main(null);
            var allMessagesCount = Program.Logger.GetAllMessages().Count;
            Assert.Equal(2, allMessagesCount);
        }

        [Fact]
        public void Main_HappyPathFileLogging_LogIsStoredInAFile()
        {
            var fileStore = new FileStore("c:\\temp\\temp.txt");
            Program.Logger = new SyncLogger(fileStore);
            Program.Main(null);

            var allMessagesCount = fileStore.GetAllMessages().Count;
            Assert.Equal(2, allMessagesCount);
        }
    }
}
