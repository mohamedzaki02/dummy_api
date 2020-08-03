import { Component, OnInit } from '@angular/core';
import { UsersService } from 'src/app/services/users.service';
import { ActivatedRoute, Data } from '@angular/router';
import { User } from 'src/app/models/user';
import { AlertifyService } from 'src/app/services/alertify.service';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation } from '@kolkov/ngx-gallery';

@Component({
  selector: 'app-member-details',
  templateUrl: './member-details.component.html',
  styleUrls: ['./member-details.component.css']
})
export class MemberDetailsComponent implements OnInit {

  constructor(private usersService: UsersService, private route: ActivatedRoute, private alertify: AlertifyService) { }
  user: User;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];

  loadUser(): void {
    this.route.data.subscribe((resolveData: Data) => this.user = resolveData['user']);
  }

  loadUserPhotos() {
    var photos = [];
    for (const photo of this.user.photos) {
      photos.push({
        small: photo.url,
        medium: photo.url,
        big: photo.url,
        description: photo.id + ' --- ' + photo.createdAt
      });
    }
    console.log(photos);
    return photos;
  }

  ngOnInit(): void {
    this.loadUser();
    this.galleryOptions = [
      {
        width: '500px',
        height: '500px',
        imagePercent : 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview:false
      }
    ];

    this.galleryImages = this.loadUserPhotos();



  }





}
