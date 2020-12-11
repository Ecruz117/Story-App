import { Component, Inject, OnInit} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';



@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html',
  styleUrls: ['./fetch-data.css']
})
export class FetchDataComponent implements OnInit {
  public news: HackerNews[];
  term: string;
  story: string;
  p: number = 1;
  public selectedStory;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    
      http.get<HackerNews[]>(baseUrl + 'hackernews').subscribe(result => {
        this.news = result;
      }, error => console.error(error));
    
  }
  ngOnInit(): void {
    this.selectedStory = this.news; 
  
  }

 





}

interface HackerNews {
  id: number;
  title: string;
  by: string;
  url: string;
  text: string;
}
