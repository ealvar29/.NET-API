<template>
    <h1 id="tableLabel">National Parks</h1>

    <p>This component demonstrates fetching National Parks from the C# server.</p>

    <p v-if="!parks"><em>Loading...</em></p>
    <button v-on:click="this.createPark">Click to add new park</button>
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
                <td>{{ getHumanDate(park.created) }}</td>
                <td>{{ getHumanDate(park.established) }}</td>
            </tr>
        </tbody>
    </table>
</template>


<script>
    import axios from 'axios'
    import moment from 'moment'
    export default {
        name: "FetchParks",
        data() {
            return {
                parks: [],
                park: {

                }
            }
        },
        methods: {
          async getNationalParks() {
                const response = await axios.get('/nationalparks');
                this.parks = response.data;
            },
            createPark() {
            // Simple POST request with a JSON body using axios
                const article = {
                    "name": "ILL",
                    "state": "Illinois",
                    "created": "2019-12-09",
                    "established": "2019-12-08"
                };
                console.log(article)
                axios.post("/nationalparks", article)
                    .then(response => {
                        console.log(response);
                        console.log('Submit Success');
                        this.getNationalParks();
                }).catch(e => {
                    console.log(e);
                });
            },
            getHumanDate(date) {
                return moment(date, 'YYYY-MM-DD').format('MM/DD/YYYY');
            }

        },
        mounted() {
            this.getNationalParks();
        }
    }
</script>