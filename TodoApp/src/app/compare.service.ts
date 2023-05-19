import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CompareService {
 // result: any[];
  constructor() { }
  
  compareArrays(array1: string | any[], array2: string | any[]) {
    var result = [];
  
    // Find the longer array
   // var longerArray = array1.length > array2.length ? array1 : array2;
    var longerArray = Math.max(array1.length, array2.length);
  
    // Iterate over the longer array
    for (var i = 0; i < longerArray; i++) {
      var item1 = array1[i];
      var item2 = array2[i];
  
      // Check if both arrays have elements at the current index
      if (item1 && item2) {
        // Compare the elements
        if (item1 === item2) {
          // If the elements match, add them as they are to the result
         // result.push(item2);
        } else {
          // If the elements don't match, add them to the result with highlighting
          //result.push('<span style="color: red">${item1}</span>');
          result.push(item2);
        }
       } 
       //else {
      //   // If one array is shorter, add the element from the longer array
      //   result.push(item1 || item2);
      // }
    }
  
    return result;
  }
}
