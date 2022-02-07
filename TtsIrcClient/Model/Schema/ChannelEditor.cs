﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace TtsIrcClient.Model.Schema
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class ChannelEditor
    {
        [Required]
        [ForeignKey("Channel")]
        public int ChannelId { get; set; }

        public virtual Channel Channel { get; set; }

        [Required]
        public int UserId { get; set; }

        protected internal static void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChannelEditor>(entity =>
            {
                entity.HasKey(nameof(ChannelId), nameof(UserId));
            });
        }
    }
}
