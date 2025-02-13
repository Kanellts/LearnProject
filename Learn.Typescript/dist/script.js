"use strict";
// import axios from 'axios';
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
const saveButton = document.querySelector('#btnSave');
const deleteButton = document.querySelector('#btnDelete');
const studentName = document.querySelector('#name');
const studentAge = document.querySelector('#age');
const studentsContainer = document.querySelector('#students_container');
if (!saveButton || !deleteButton || !studentName || !studentAge || !studentsContainer) {
    throw new Error("One or more required elements are missing in the DOM.");
}
function clearForm() {
    return __awaiter(this, void 0, void 0, function* () {
        studentName.value = '';
        studentAge.value = '';
        deleteButton.classList.add('hidden');
    });
}
function displayStudentInForm(student) {
    return __awaiter(this, void 0, void 0, function* () {
        studentName.value = student.name;
        studentAge.value = student.age.toString();
        deleteButton.classList.remove('hidden');
        saveButton.setAttribute('data-id', student.id.toString());
        deleteButton.setAttribute('data-id', student.id.toString());
    });
}
function getStudentById(id) {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            const response = (yield axios.get(`https://localhost:7103/api/Students/${id}`));
            displayStudentInForm(response.data);
        }
        catch (error) {
            console.error("Error fetching students: ", error);
        }
    });
}
function populateForm(id) {
    getStudentById(id);
}
function displayStudents(students) {
    let allStudents = '';
    students.forEach((student) => {
        const studentElement = `
                                    <div class="student" data-id="${student.id}">
                                        <h3>${student.name}</h3>
                                        <p>${student.age}</p>
                                    </div>
                                `;
        allStudents += studentElement;
    });
    studentsContainer.innerHTML = allStudents;
    //add event listener
    document.querySelectorAll('.student').forEach(student => {
        student.addEventListener('click', function () {
            return __awaiter(this, void 0, void 0, function* () {
                populateForm(Number(student.getAttribute("data-id")));
            });
        });
    });
}
function getAllStudents() {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            const response = yield axios.get('https://localhost:7103/api/Students');
            displayStudents(response.data);
        }
        catch (error) {
            console.error("Error fetching students: ", error);
        }
    });
}
function createStudent(name, age) {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            yield axios.post('https://localhost:7103/api/Students', { name, age }, { headers: { "content-type": "application/json" } });
            clearForm();
            getAllStudents();
        }
        catch (error) {
            console.error("Error creating student: ", error);
        }
    });
}
function updateStudent(id, name, age) {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            yield axios.put(`https://localhost:7103/api/Students/${id}`, { name, age }, { headers: { "content-type": "application/json" } });
            clearForm();
            getAllStudents();
        }
        catch (error) {
            console.error("Error updating student: ", error);
        }
    });
}
saveButton.addEventListener('click', function () {
    const id = saveButton.dataset.id;
    if (id) {
        updateStudent(Number(id), studentName.value, Number(studentAge.value));
    }
    else {
        createStudent(studentName.value, Number(studentAge.value));
    }
});
function deleteStudent(id) {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            yield axios.delete(`https://localhost:7103/api/Students/${id}`);
            clearForm();
            getAllStudents();
        }
        catch (error) {
            console.error("Error deleting student: ", error);
        }
    });
}
deleteButton.addEventListener('click', function () {
    const id = deleteButton.dataset.id;
    deleteStudent(Number(id));
});
getAllStudents();
