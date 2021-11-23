<template>
    <h1 id="tableLabel">National Parks</h1>

    <p>This component demonstrates fetching National Parks from the C# server.</p>

    <p v-if="!parks"><em>Loading...</em></p>

    <table class='table table-striped' aria-labelledby="tableLabel" v-if="parks">
        <thead>
            <tr>
                <th>Id</th>
                <th>National Park</th>
                <th>State</th>
                <th>Created</th>
                <th>Established</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="park of parks" v-bind:key="park">
                <td>{{ park.id }}</td>
                <td>{{ park.name }}</td>
                <td>{{ park.state }}</td>
                <td>{{ park.created }}</td>
                <td>{{ park.established }}</td>
            </tr>
        </tbody>
    </table>
</template>


<script>
    import axios from 'axios'
    export default {
        name: "FetchParks",
        data() {
            return {
                parks: []
            }
        },
        methods: {
            getNationalParks() {
                axios.get('/nationalparks')
                    .then((response) => {
                        this.parks = response.data;
                        console.log(this.parks[0])
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            }
        },
        mounted() {
            this.getNationalParks();
        }
    }
</script>