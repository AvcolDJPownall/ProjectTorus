﻿using Microsoft.EntityFrameworkCore;
using Torus.Data;
using Torus.Models;

namespace Torus.Areas.Identity.Data
{
    public class TorusInitializer
    {
        private TorusContext TorusContext;
        public void SeedDatabase(TorusContext context)
        {
            TorusContext = context;
            InternalSeedPosts();
        }

        private void InternalSeedPosts()
        {
            if (TorusContext.TorusPost != null && TorusContext.TorusPost.Any()) return;

            TorusPost[] initposts = new TorusPost[3]{
                new TorusPost() { Title = "Chair", Cost = 1.00f, PostType = AssetType.Mesh, Description = "A simple chair mesh with a low polygon count.", ImageFileGUID = "item-1c6ce0bf-36c0-4199-9440-e2166e4fecac", AssetFileGUID = "chair-edb2ef00d6d6.zip", AuthorID = Guid.NewGuid().ToString()},
                new TorusPost() { Title = "Space Junk", Cost = 0.00f, PostType = AssetType.Mesh, Description = "Abstract collection of objects.", ImageFileGUID = "liminal_junk", AuthorID = Guid.NewGuid().ToString()},
                new TorusPost() { Title = "Glossy Surface", Cost = 29.99f, PostType = AssetType.Shader, Description = "A high-performance, glossy surface shader with normals.\nTinted by the vertex color.", ImageFileGUID = "shader_ngloss", AuthorID = Guid.NewGuid().ToString()},
            };

            //TorusContext.TorusPost.RemoveRange(TorusContext.TorusPost.AsEnumerable());
            TorusContext.TorusPost.AddRange(initposts);
            TorusContext.SaveChanges();

        }

        //internal static TorusUser GenerateTorusUser(string uname, string userID, float balance, string email = "")
        //{
        //    TorusUser user = new TorusUser();
        //    user.UserName = "";
        //    user.Email = (email == "" ? uname.ToLower() + "@torus.example" : email);
        //    user.PasswordHash = Guid.NewGuid().ToString();
        //    user.Balance = (long)(balance * 100.0f);
        //    return user;
        //}

        //internal static TorusPost GenerateTorusPost(string title, float cost, string authorId, AssetType postType, string description = "A Torus post.")
        //{
        //    TorusPost post = new TorusPost();
        //    post.Title = title;
        //    post.Cost = cost;
        //    post.Description = description;
        //    post.ImageFileGUID = Guid.NewGuid().ToString();
        //    post.AuthorID = authorId;
        //    return post;
        //}

    }
}