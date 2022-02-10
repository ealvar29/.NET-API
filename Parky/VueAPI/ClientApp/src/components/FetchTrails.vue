<template>
    <h1 id="tableLabel">National Park Trails</h1>

    <p>This component demonstrates fetching National Park Trails from the C# Controller.</p>

    <p v-if="!trails"><em>Loading...</em></p>

    <form class="w-full max-w-lg inline-block p-10" @submit.prevent="onSubmit">
        <div class="flex flex-wrap -mx-3 mb-6">
            <div class="w-full md:w-1/2 px-3 mb-6 md:mb-0">
                <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-first-name">
                    Name of National Park Trail:
                </label>
                <input v-model="name" class="appearance-none block w-full bg-gray-200 text-gray-700 border border-red-500 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white" id="grid-first-name" type="text" >
            </div>
            <div class="w-full md:w-1/2 px-3">
                <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-last-name">
                    State:
                </label>
                <input v-model="state" class="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500" id="grid-last-name" type="text">
            </div>
        </div>
        <div class="flex flex-wrap -mx-3 mb-6">
            <div class="w-full md:w-1/2 px-3 mb-6 md:mb-0">
                <label type="date" class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-first-name">
                    Date Created:
                </label>
                <input type="date" v-model="created" class="appearance-none block w-full bg-gray-200 text-gray-700 border border-red-500 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white" >
            </div>
            <div class="w-full md:w-1/2 px-3">
                <label type="date" class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-last-name">
                    Date established:
                </label>
                <input type="date" v-model="established" class="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500" >
            </div>
        </div>

        <div class="md:flex md:items-center">
            <div class="md:w-full">
                <button type="submit" value="Submit" class="shadow bg-purple-500 hover:bg-purple-400 focus:shadow-outline focus:outline-none text-white font-bold py-2 px-4 rounded">
                    Submit
                </button>
            </div>
        </div>

    </form>

    <table class='table table-striped' aria-labelledby="tableLabel" v-if="trails">
        <thead>
            <tr>
                <th>Id</th>
                <th>National Park Trail</th>
                <th>National Park Id</th>
                <th>Distance</th>
                <th>Difficulty</th>
                <th>Date Created</th>
                <th>Edit</th>
                <th>Delete</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="trail of trails" :key="trail.id">
                <td>{{ trail.id }}</td>
                <td>{{ trail.name }}</td>
                <td>{{ trail.nationalparkid }}</td>
                <td>{{ trail.distance }}</td>
                <td>{{ trail.difficulty }}</td>
                <td>{{ getHumanDate(trail.created) }}</td>
                <td>
                    <button class="bg-indigo-400 hover:bg-indigo-500 text-white font-bold py-2 px-4 rounded">
                        Edit
                    </button>
                </td>
                <td>
                    <button @click="deletePark(park.id)" class="bg-red-400 hover:bg-red-500 text-white font-bold py-2 px-4 rounded">
                        Delete
                    </button>
                </td>
            </tr>
        </tbody>
    </table>
</template>


<script>
    import axios from 'axios'
    import moment from 'moment'
    export default {
        name: "FetchTrails",
        mounted() {
            this.getTrails();
        },
        data() {
            return {
                trails: [],
                name: '',
                distance: 0,
                difficulty: 0,
                nationalParkId: 0,
                created: null,
            }
        },
        methods: {
            onSubmit() {
                let newTrail = {
                    name: this.name,
                    state: this.state,
                    created: this.created,
                    established: this.established
                }
                axios.post("/nationalparks", newTrail)
                    .then(response => {
                        console.log(response);
                        console.log('Submit Success');
                        this.getTrails();
                    }).catch(e => {
                        console.log(e);
                    });
                this.$emit('', newTrail);
                this.name = '';
                this.state = '';
                this.created = null;
                this.established = null;
            },
          async getTrails() {
                const response = await axios.get('api/v1/trails');
                this.trails = response.data;
            },
            createPark() {
            // Simple POST request with a JSON body using axios
                const trail = {
                    "name": "ILL",
                    "state": "Illinois",
                    "created": "2019-12-09",
                    "established": "2019-12-08"
                };
                axios.post("/api/trails", trail)
                    .then(response => {
                        console.log(response);
                        console.log('Submit Success');
                        this.getTrails();
                }).catch(e => {
                    console.log(e);
                });
            },
            getHumanDate(date) {
                return moment(date, 'YYYY-MM-DD').format('MM/DD/YYYY');
            },
            deletePark(parkId) {
                console.log(parkId);
                axios.delete(`/nationalparks/1`)
                    .then(response => {
                        console.log(response);
                        console.log('Submit Success');
                    }).catch(e => {
                        console.log(e);
                    });
                this.getTrails();
            }
        }
    }
</script>