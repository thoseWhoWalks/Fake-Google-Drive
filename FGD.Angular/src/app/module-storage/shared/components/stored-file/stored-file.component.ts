import { Component, OnInit, Input } from '@angular/core';
import { Store } from '@ngrx/store';
import { FileState } from 'src/app/redux/app.state';
import { Observable } from 'rxjs';
import { Files } from 'src/app/redux/transfer-models/file.model.transfer';
import { FileModel } from '../../models/file.model';
import { FilePageAction } from 'src/app/redux/actions/file.action';
import { selectFiles } from 'src/app/redux/selectors/file.selector';

@Component({
  selector: 'app-stored-file',
  templateUrl: './stored-file.component.html',
  styleUrls: ['./stored-file.component.css']
})
export class StoredFileComponent implements OnInit {

  constructor(private store: Store<FileState>) { }

  filesState: Observable<Files>;

  ngOnInit() {
    this.filesState = this.store.select(selectFiles); 
    this.resolveThumbnail()
  } 

  public isSelected:boolean = false;

  @Input() 
  file: FileModel = null;  
   
  thumbnailSrc: string = "assets/default-thumbnails/default-thumbnail.png";  

  onFileClicked(){
    console.log(this.file);
    this.isSelected = !this.isSelected;

     this.store.dispatch(FilePageAction.process_selection_file(
          this.file
     ))
  }

  private resolveThumbnail(){

    let def = "assets/default-thumbnails/default-thumbnail.jpg"
    let audioThumb = "assets/default-thumbnails/audio-thumbnail.jpg"
    let dockThumb = "assets/default-thumbnails/dockument-thumbnail.png"

    if(this.file.thumbnailPath !== null)
      return

    switch(this.file.extention){
      case ".mp3" : this.file.thumbnailPath = audioThumb; break;
      case ".txt" : this.file.thumbnailPath = dockThumb; break;
      case ".doc" : this.file.thumbnailPath = dockThumb; break;
      case ".docx" : this.file.thumbnailPath = dockThumb; break;
      default : this.file.thumbnailPath = def; 
    }

  }

}
