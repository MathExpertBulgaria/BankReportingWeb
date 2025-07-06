import { TransactionNomenModel } from "./transaction-nomen.model";

export interface SearchTransactionModel {
    idMerchant?: number;
    createDateFrom?: Date;
    createDateTo?: Date;
    idDirection?: number;
    amountFrom?: number;
    amountTo?: number;
    idCcy?: number;
    debtorIban?: string;
    beneficiaryIban?: string;
    status?: boolean;
    externalId?: string;

    nomen?: TransactionNomenModel;
}
