"use strict";
// import { GridApi, GridOptions, createGrid, AllCommunityModule, ModuleRegistry } from '../node_modules/ag-grid-community';
// import '../node_modules/ag-grid-community/styles/ag-grid.css'
// import '../node_modules/ag-grid-community/styles/ag-theme-balham.css'
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
const gridOptions = {
    // Grid Options: Contains all of the Data Grid configurations
    columnDefs: [
        { headerName: 'Id', field: 'id', sortable: true, filter: true },
        { headerName: 'Name', field: 'name', sortable: true, filter: true },
        { headerName: 'Age', field: 'age', sortable: true, filter: true }
    ],
    rowData: null
    // [
    //     { id: "1", name: "Kanellos", age: "45"},
    //     { id: "2", name: "Kanell ts", age: "45"},
    //     { id: "3", name: "Kanellos safas", age: "45"}
    // ]
};
const studentsGrid = document.querySelector("#studentsGrid");
const gridApi = agGrid.createGrid(studentsGrid, gridOptions);
function getAllStudents() {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            const response = yield axios.get('https://localhost:7103/api/Students');
            gridApi.setGridOption('rowData', response.data);
        }
        catch (error) {
            console.error("Error fetching students: ", error);
        }
    });
}
getAllStudents();
// async function clearForm() {
//     studentName!.value = '';
//     studentAge!.value = '';
//     deleteButton!.classList.add('hidden');
// }
// async function displayStudentInForm(student: { name: string; age: number; id: number; }) {
//     studentName!.value = student.name;
//     studentAge!.value = student.age.toString();
//     deleteButton!.classList.remove('hidden');
//     saveButton!.setAttribute('data-id',student.id.toString());
//     deleteButton!.setAttribute('data-id',student.id.toString());
// }
// async function getStudentById(id: number) {
//     try {
//         const response = (await axios.get<Student>(`https://localhost:7103/api/Students/${id}`));
//         displayStudentInForm(response.data);
//     }
//     catch(error) {
//         console.error("Error fetching students: ", error);
//     }
// }
// function populateForm(id: number) {
//     getStudentById(id);
// }
// function displayStudents(students: Student[]) {
//     let allStudents = '';
//     students.forEach((student) => {
//         const studentElement = `
//                                     <div class="student" data-id="${student.id}">
//                                         <h3>${student.name}</h3>
//                                         <p>${student.age}</p>
//                                     </div>
//                                 `;
//         allStudents += studentElement        
//     });
//     studentsContainer!.innerHTML = allStudents;
//     //add event listener
//     document.querySelectorAll('.student').forEach(student => {
//         student.addEventListener('click', async function() {
//             populateForm(Number(student!.getAttribute("data-id")));
//         });
//     });
// }
// async function getAllStudents() {
//     try {
//         const response = await axios.get<Student[]>('https://localhost:7103/api/Students');
//         displayStudents(response.data);
//     } 
//     catch(error) {
//         console.error("Error fetching students: ", error);
//     }
// }
// async function createStudent(name: string, age: number) {
//     try {
//         await axios.post<StudentDTO>('https://localhost:7103/api/Students', { name, age }, {headers: {"content-type": "application/json"}});
//         clearForm();
//         getAllStudents();
//     }
//     catch(error) {
//         console.error("Error creating student: ", error);
//     }
// }
// async function updateStudent(id: number, name: string, age: number) {
//     try {
//         await axios.put<StudentDTO>(`https://localhost:7103/api/Students/${id}`, { name, age }, { headers: {"content-type": "application/json"} });
//         clearForm();
//         getAllStudents();
//     }
//     catch(error) {
//         console.error("Error updating student: ", error);
//     }
// }
// saveButton.addEventListener('click', function() {
//     const id = saveButton.dataset.id;
//     if(id) {
//         updateStudent(Number(id),studentName!.value, Number(studentAge!.value));
//     }
//     else {
//         createStudent(studentName!.value, Number(studentAge!.value));
//     }
// });
// async function deleteStudent(id: number) {
//     try {
//         await axios.delete(`https://localhost:7103/api/Students/${id}`);
//         clearForm();
//         getAllStudents();
//     }
//     catch(error) {
//         console.error("Error deleting student: ", error);
//     }
// }
// deleteButton.addEventListener('click', function() {
//     const id = deleteButton.dataset.id;
//     deleteStudent(Number(id));
// });
// getAllStudents();
