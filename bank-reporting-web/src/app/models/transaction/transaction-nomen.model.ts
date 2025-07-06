import { NomenModel } from "../core/nomen.model";

export interface TransactionNomenModel {
    nCurrency: NomenModel<number>[];
    nTransactionDirection: NomenModel<number>[];
}