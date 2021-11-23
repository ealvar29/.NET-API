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
                <td>{{ park.Id }}</td>
                <td>{{ park.Name }}</td>
                <td>{{ park.State }}</td>
                <td>{{ park.Created }}</td>
                <td>{{ park.Established }}</td>
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
                        this.parks =  response.data;
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