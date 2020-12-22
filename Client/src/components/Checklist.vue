<template>
    <div class="container">
        <h2>Task Items</h2>
        <br />
        <div class="input">
            <input type="text" v-model="newItemText">
            <button type="button" class="btn-info" @click="onClickSave">Create</button>
        </div>
        <br />
        <table class='table table-bordered table-strripped table-hover'>
            <thead>
                <tr>
                    <th>To Do Item</th>
                    <th>Actions</th>
                </tr>
            </thead>

            <tbody v-for="item in items" :key="item.id">
                <tr>
                    <td>
                        {{ item.text }}
                    </td>
                    <td>
                        <button class='btn btn-danger' @click="onClickDelete(item.id)"> Delete </button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                newItemText: '',
                items: []
            }
        },
        async mounted() {
            await this.loadItems()
        },
        methods: {
            async onClickSave() {
                await fetch('https://localhost:5001/api/checklist', {
                    method: 'POST',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({ text: this.newItemText })
                })
                this.newItemText = ''
                await this.loadItems()
            },
            async loadItems() {
                const res = await fetch('https://localhost:5001/api/checklist')
                this.items = await res.json()
            },
            async onClickDelete(id) {
                await fetch(`https://localhost:5001/api/checklist/${id}`, {
                    method: 'DELETE'
                })
                await this.loadItems()
            }
        }
    }
</script>