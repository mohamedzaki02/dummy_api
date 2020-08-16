import { Component, OnInit, Input } from '@angular/core';
import { Photo } from 'src/app/models/Photo';
import { FileUploadControl, FileUploadValidators } from '@iplab/ngx-file-upload';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {

  constructor() { }
  @Input() photos: Photo[];
  uploadedFiles: File[];
  isDisabled = true;
  isMultiple = true;

  ngOnInit(): void {
  }

  public fileUploadControl = new FileUploadControl(FileUploadValidators.fileSize(80000));

    public toggleStatus() {
        this.fileUploadControl.disable(!this.fileUploadControl.disabled);
    }

    public toggleListVisibility() {
        this.fileUploadControl.setListVisibility(!this.fileUploadControl.isListVisible);
    }

    public toggleMultiple() {
        this.fileUploadControl.multiple(!this.fileUploadControl.isMultiple);
    }

    public clear(): void {
        this.fileUploadControl.clear();
    }

}
