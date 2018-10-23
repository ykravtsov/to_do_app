<template>
    <div class="container">
        <ul class="collection with-header">
            <li class="collection-header cyan"><h4>To Do Items</h4></li>
            <li class="collection-item" v-for="(toDoItem, index) in toDoItems">
                <label>
                    <input type="checkbox" v-model="toDoItem.isComplete" @change="updateToDo(toDoItem)" />
                    <span>{{toDoItem.name}}</span>
                </label>
                <a href="#!" class="secondary-content" @click="deleteToDoItem(toDoItem.toDOItemId, index)"><i class="material-icons">delete_forever</i></a>
            </li>
            <li class="collection-item">
                <div class="file-field input-field">
                    <div class="btn" @click="addToDo">
                        <span>Add To Do Item</span>
                    </div>
                    <div class="file-path-wrapper">
                        <input type="text" placeholder="Add a new todo item here" v-model="newToDoName">
                    </div>
                </div>
            </li>
        </ul>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import axios from 'axios';

@Component
export default class Home extends Vue {
    toDoItems: { toDOItemId: number, name: string, isCompleted: boolean}[] = [];
    newToDoName = '';

    async addToDo() {
        try {
            const response = await axios.post('/api/ToDo', {
                name: this.newToDoName
            })
            this.toDoItems.push(response.data);
        } catch (error) {
            console.log(error);
        }

        this.newToDoName = '';
    }

    async updateToDo(toDoItem: { toDOItemId: number, name: string, isCompleted: boolean }) {
        try {
            const response = await axios.put('/api/ToDo/' + toDoItem.toDOItemId, toDoItem);
        } catch (error) {
            console.log(error);
        }
    }

    async deleteToDoItem(toDOItemId: number, index: number) {
        try {
            const response = await axios.delete('/api/ToDo/' + toDOItemId);
            this.toDoItems.splice(index, 1);
        } catch (error) {
            console.log(error);
        }
    }

    async mounted() {
        try {
            const response = await axios.get('/api/ToDo');
            this.toDoItems = response.data;
        } catch(error) {
            console.log(error);
        }
    }
}
</script>
