import { PageModel } from "./page-model";
import { SortModel } from "./sort-model";

export interface SearchModel {
    // Page
    page?: PageModel;
    isPaging?: boolean;

    // Sort
    sort?: SortModel | null;
}