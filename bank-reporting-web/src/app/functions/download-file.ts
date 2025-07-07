import { DownloadFileModel } from "../models/download-file.model";
import { b64toBlob } from "./b64toBlob";

export function downloadFile(model: DownloadFileModel) {
    const blob = b64toBlob(model.contents, model.contentType);
    const a = document.createElement('a');
    const url = window.URL.createObjectURL(blob);

    document.body.appendChild(a);
    a.href = url;
    a.download = model.filename!;
    a.click();
  }