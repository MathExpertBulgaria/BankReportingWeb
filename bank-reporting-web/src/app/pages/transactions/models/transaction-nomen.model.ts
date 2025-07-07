import { NomenModel } from "../../../models/nomen.model";

export interface TransactionNomenModel {
    nCurrency: NomenModel<string>[];
    nTransactionDirection: NomenModel<string>[];
}