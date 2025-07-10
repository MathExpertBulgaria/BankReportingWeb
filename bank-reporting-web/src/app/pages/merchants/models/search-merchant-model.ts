import { SearchModel } from "../../../models/search-model";

export interface SearchMerchantModel extends SearchModel {
    idPartner?: number;
    name?: string;
    boardingDateFrom?: Date;
    boardingDateTo?: Date;
    country?: number;
}