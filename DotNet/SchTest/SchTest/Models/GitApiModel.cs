using Azure.Search.Documents.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchTest.Models
{

    public class GitApiModel
    {
        [SimpleField(IsKey = true, IsFilterable = true)]
        public string current_user_url;

        [SimpleField(IsFilterable = true, IsFacetable = true)]
        public string current_user_authorizations_html_url;

        [SimpleField(IsFilterable = true, IsFacetable = true)]
        public string authorizations_url;

        [SimpleField(IsFilterable = true, IsFacetable = true)]
        public string code_search_url;

        [SimpleField(IsFilterable = true, IsFacetable = true)]
        public string commit_search_url;

        [SimpleField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string emails_url;

        [SimpleField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string emojis_url;

        [SimpleField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string events_url;

        [SimpleField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string feeds_url;

        [SimpleField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string followers_url;

        [SimpleField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string following_url;

        [SimpleField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string gists_url;

        [SimpleField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string hub_url;

        [SimpleField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string issue_search_url;

        [SimpleField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string issues_url;

        [SimpleField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string keys_url;

        [SimpleField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string label_search_url;

        [SimpleField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string notifications_url;

        [SimpleField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string organization_url;

        [SimpleField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string organization_repositories_url;

        [SimpleField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string organization_teams_url;

        [SimpleField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string public_gists_url;

        [SimpleField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string rate_limit_url;

        [SimpleField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string repository_url;

        [SimpleField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string repository_search_url;

        [SimpleField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string current_user_repositories_url;

        [SimpleField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string starred_url;

        [SimpleField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string starred_gists_url;

        [SimpleField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string user_url;

        [SimpleField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string user_organizations_url;

        [SimpleField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string user_repositories_url;

        [SimpleField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string user_search_url;
    }
}
