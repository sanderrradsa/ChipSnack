using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace WebdevProjectStarterTemplate.Pages.Snacks
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public IEnumerable<Snack> SelectedSnacks;
        public IEnumerable<Snackbar> SnackbarFilters;
        public IEnumerable<Categorie> CategoryFilters;

        public SnackReposiroty SnackRepository;
        public SnackbarRepository SnackbarRepository;
        public CategorieRepository CategorieRepository;

        public int selectedSnackbarId = -1;
        public int selectedCategoryId = -1;
        public void OnGet(int? snackbarID = null, int? categoryId = null)
        {
            SnackRepository = new SnackReposiroty();
            SnackbarRepository = new SnackbarRepository();
            CategorieRepository = new CategorieRepository();

            selectedSnackbarId = snackbarID ?? -1;
            selectedCategoryId = categoryId ?? -1;

            if (snackbarID is not null && categoryId is not null)
            {
                SelectedSnacks = SnackRepository.GetFromBarAndCategory((int)snackbarID, (int)categoryId);
            }
            else if (snackbarID is not null)
            {
                SelectedSnacks = SnackRepository.GetFromSnackBar((int)snackbarID);
            }
            else if (categoryId is not null)
            {
                SelectedSnacks = SnackRepository.GetFromCategory((int)categoryId);
            }
            else
            {
                SelectedSnacks = SnackRepository.Get();
            }
            
            SnackbarFilters = SnackbarRepository.Get();
            CategoryFilters = CategorieRepository.Get();
        }

    }
}
