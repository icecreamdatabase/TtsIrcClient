using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using TtsIrcClient.Model.Schema;

namespace TtsIrcClient.Model
{
    public sealed class TtsDbContext : DbContext
    {
        /// <summary>
        /// <c>TreatTinyAsBoolean=false</c> results in using bit(1) instead of tinyint(1) for <see cref="bool"/>.<br/>
        /// <br/>
        /// In 5.0.5 SSL was enabled by default. It isn't necessary for our usage.
        /// (We don't expose the DB to the internet.)
        /// https://stackoverflow.com/a/45108611
        /// </summary>
        private const string AdditionalMySqlConfigurationParameters = ";TreatTinyAsBoolean=false;SslMode=none";

        private readonly string _fullConString;

        public DbSet<BotData> BotData { get; set; } = null!;
        public DbSet<GlobalUserBlacklist> GlobalUserBlacklist { get; set; } = null!;
        public DbSet<Channel> Channels { get; set; } = null!;
        public DbSet<ChannelEditor> ChannelEditors { get; set; } = null!;
        public DbSet<ChannelUserBlacklist> ChannelUserBlacklist { get; set; } = null!;
        public DbSet<BotSpecialUser> BotSpecialUsers { get; set; } = null!;

        public TtsDbContext()
        {
            string? dbConString = Program.ConfigRoot.ConnectionStrings.TtsDb;
            if (string.IsNullOrEmpty(dbConString))
                throw new InvalidOperationException("No MySql connection string!");
            _fullConString = dbConString + AdditionalMySqlConfigurationParameters;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_fullConString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Schema.BotData.BuildModel(modelBuilder);
            Schema.GlobalUserBlacklist.BuildModel(modelBuilder);
            Schema.Channel.BuildModel(modelBuilder);
            Schema.ChannelEditor.BuildModel(modelBuilder);
            Schema.ChannelUserBlacklist.BuildModel(modelBuilder);
            Schema.BotSpecialUser.BuildModel(modelBuilder);
        }
    }
}
