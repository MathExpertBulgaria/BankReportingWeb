import { SearchModel } from "../../../models/search-model";
import { TransactionNomenModel } from "./transaction-nomen.model";

export interface SearchTransactionModel extends SearchModel {
    idPartner?: number;
    partnerName?: string;
    idMerchant?: number;
    merchantName?: string;
    createDateFrom?: Date;
    createDateTo?: Date;
    idDirection?: string;
    amountFrom?: number;
    amountTo?: number;
    idCcy?: string;
    debtorIban?: string;
    beneficiaryIban?: string;
    status?: boolean;
    externalId?: string;

    nomen?: TransactionNomenModel;
}
