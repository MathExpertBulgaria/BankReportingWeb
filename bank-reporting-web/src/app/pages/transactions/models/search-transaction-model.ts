import { TransactionNomenModel } from "./transaction-nomen.model";

export interface SearchTransactionModel {
    idMerchant?: number;
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
