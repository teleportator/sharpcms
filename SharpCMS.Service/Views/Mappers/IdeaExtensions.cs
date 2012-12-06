using System;
using System.Collections.Generic;
using System.Linq;
using SharpCMS.Domain;

namespace SharpCMS.BusinessLogic.Views.Mappers
{
	public static class IdeaExtensions
	{
		public static IdeaView ConvertToIdeaView(this Idea idea)
		{
			if (idea == null)
				throw new ArgumentNullException();

			return new IdeaView
			       	{
			       		Id = idea.Id,
			       		Title = idea.Title,
			       		ParentId = idea.ParentId,
			       		Abstract = idea.Abstract,
			       		Created = idea.Created,
			       		CreatedBy = idea.CreatedBy,
			       		IsActive = idea.IsActive,
			       		Text = idea.Text,
			       		Updated = idea.Updated,
			       		UpdatedBy = idea.UpdatedBy,
			       		Category = idea.Category,
						Rating = idea.Rating,
						DisplayOnMainMenu = idea.DisplayOnMainMenu,
						DisplayOnSideMenu = idea.DisplayOnSideMenu,
						SortOrder = idea.SortOrder,
						Url = idea.Url
			       	};
		}

		public static IEnumerable<IdeaView> ConvertToIdeaViewCollection(this IEnumerable<Idea> ideas)
		{
			return ideas.Select(i => i.ConvertToIdeaView()).ToList();
		}
	}
}