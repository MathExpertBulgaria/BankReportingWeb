export interface TransactionModel {
    id?: number;
    partnerName: string;
    idMerchant: number;
    merchantName?: string;
    createDate: Date;
    idDirection?: string;
    amount?: number;
    idCcy?: string;
    debtorIban?: string;
    beneficiaryIban?: string;
    status?: string;
    externalId?: string;
    idTransactionFile?: number;
}