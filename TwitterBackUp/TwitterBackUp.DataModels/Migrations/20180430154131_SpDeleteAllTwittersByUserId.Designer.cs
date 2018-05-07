﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using TwitterBackUp.DataModels.Models;

namespace TwitterBackUp.DataModels.Migrations
{
    [DbContext(typeof(TwitterContext))]
    [Migration("20180430154131_SpDeleteAllTwittersByUserId")]
    partial class SpDeleteAllTwittersByUserId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TwitterBackUp.DomainModels.Tweet", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(512);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int?>("RetweetsCount");

                    b.Property<string>("TwitterId");

                    b.Property<string>("TwitterScreenName");

                    b.HasKey("Id");

                    b.ToTable("Tweets");
                });

            modelBuilder.Entity("TwitterBackUp.DomainModels.Twitter", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("Description")
                        .HasMaxLength(250);

                    b.Property<int?>("FollowersCount");

                    b.Property<int?>("FriendsCount");

                    b.Property<string>("ProfileBackgroundImageUrl");

                    b.Property<string>("ProfileImageUrl");

                    b.Property<string>("ScreenName")
                        .IsRequired()
                        .HasMaxLength(125);

                    b.Property<string>("Url");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(125);

                    b.HasKey("Id");

                    b.HasAlternateKey("ScreenName")
                        .HasName("AlternateKey_ScreenName");

                    b.ToTable("Twitters");
                });

            modelBuilder.Entity("TwitterBackUp.DomainModels.UsersTweets", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("TweetId");

                    b.HasKey("UserId", "TweetId");

                    b.HasIndex("TweetId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersTweets");
                });

            modelBuilder.Entity("TwitterBackUp.DomainModels.UsersTwitters", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("TwitterId");

                    b.HasKey("UserId", "TwitterId");

                    b.HasIndex("TwitterId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersTwitters");
                });

            modelBuilder.Entity("TwitterBackUp.DomainModels.UsersTweets", b =>
                {
                    b.HasOne("TwitterBackUp.DomainModels.Tweet", "Tweet")
                        .WithMany("UsersTweets")
                        .HasForeignKey("TweetId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("TwitterBackUp.DomainModels.UsersTwitters", b =>
                {
                    b.HasOne("TwitterBackUp.DomainModels.Twitter", "Twitter")
                        .WithMany("UsersTwitters")
                        .HasForeignKey("TwitterId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}