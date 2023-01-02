import { SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Router } from '@angular/router';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(private socialAuthService: SocialAuthService, private httpClient: HttpClient,    private router: Router,
    ) {
    this.socialAuthService.authState.subscribe((user: SocialUser) => {
      httpClient.post("http://localhost:5232/api/user/", user).subscribe(token => {
        if (token) {
          this.router.navigate(['/', 'home']);
        }
      })
      
    });
  }
 
  title = 'google-auth';
}
