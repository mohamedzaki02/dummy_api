import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Photo } from 'src/app/models/Photo';
import { FileUploadControl, FileUploadValidators } from '@iplab/ngx-file-upload';
import { HttpEventType, HttpErrorResponse } from '@angular/common/http';
import { UploadService } from '../../../services/upload.service';
import { of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { UsersService } from 'src/app/services/users.service';
import { AuthService } from 'src/app/services/auth.service';
import { AlertifyService } from 'src/app/services/alertify.service';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {

  constructor(private uploadService: UploadService,
    private userService: UsersService,
    private authService: AuthService,
    private alertify: AlertifyService) { }


  @Input() photos: Photo[];
  @Output() photoUploaded = new EventEmitter<Photo>();

  uploadedFiles: File[];
  isDisabled = true;
  isMultiple = true;
  inProgress = false;
  progress: number = 0;

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

  uploadFiles() {
    this.fileUploadControl.value.forEach((file) => {
      const formData = new FormData();
      formData.append('file', file);
      this.inProgress = true;
      this.uploadService.upload(formData).pipe(
        map(event => {
          switch (event.type) {
            case HttpEventType.UploadProgress:
              console.log(Math.round(event.loaded * 100 / event.total));
              break;
            case HttpEventType.Response:
              return event;
          }
        }),
        catchError((error: HttpErrorResponse) => {
          this.inProgress = false;
          return of(`${file.name} upload failed.`);
        })).subscribe((event: any) => {
          if (typeof (event) === 'object') {
            this.photoUploaded.emit(event.body);
            this.fileUploadControl.removeFile(file);
          }
        });
    });

  }

  setMainPhoto(photoId) {
    this.userService.setUserMainPhoto(this.authService.currentUser.userId, photoId).subscribe(
      () => {
        this.photos.forEach(usrPhoto => {
          if (photoId == usrPhoto.id) usrPhoto.isMain = true;
          else usrPhoto.isMain = false;
        });
        this.alertify.success('photo updated successfully!');
      },
      error => this.alertify.error(error)
    );
  }

}
