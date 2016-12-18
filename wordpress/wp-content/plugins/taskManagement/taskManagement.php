<?php
    /*
    Plugin Name: Task Management
    Description: Plugin that offers full crud with the assessment task management api
    Author: Stafleu Buitendag
    */

    // Method: POST, PUT, GET etc
    // Data: array("param" => "value") ==> index.php?param=value

    global $tasks, $task;

    function CallAPI($method, $url, $data = false)
    {
        $curl = curl_init();
    
        switch ($method)
        {
            case "POST":
                curl_setopt($curl, CURLOPT_POST, 1);

                if ($data)
                    curl_setopt($curl, CURLOPT_POSTFIELDS, $data);
                break;
            case "PUT":
                curl_setopt($curl, CURLOPT_PUT, 1);
                break;
            default:
                if ($data)
                    $url = sprintf("%s?%s", $url, http_build_query($data));
        }

        // Optional Authentication:
        curl_setopt($curl, CURLOPT_HTTPAUTH, CURLAUTH_BASIC);
        curl_setopt($curl, CURLOPT_USERPWD, "");

        curl_setopt($curl, CURLOPT_URL, $url);
        curl_setopt($curl, CURLOPT_RETURNTRANSFER, 1);

        $result = curl_exec($curl);

        curl_close($curl);

        return $result;
    }

    //return [{id, name, status}]
    function fetch_tasks() {
        $tasks = CallAPI("GET", "http://localhost/taskManagement/api/tasks");


        echo $tasks;//"[{id, name, status}]";
        return $tasks;
    }

    //return {id, name, description, dueDate, creationDate, status}
    function fetch_task( $id ) {
        $task = CallAPI("GET", "http://localhost/taskManagement/api/tasks/" . $id);

        echo $task;
    }

    //return {id, name, description, dueDate, creationDate, status}
    function update_task( $task ) {
        $success = CallAPI("POST", "http://localhost/taskManagement/api/tasks", $task);

        echo $success;
    }

    //return {id, name, description, dueDate, creationDate, status}
    // $name, $description, $dueDate, $status
    function create_task( $task ) {
        $success = CallAPI("PUT", "http://localhost/taskManagement/api/tasks", $task);

        echo $success;
    }

    function remove_task( $id ) {
        $success = CallAPI("DELETE", "http://localhost/taskManagement/api/tasks", $id);

        echo $success;
    }

    // add_action( 'admin_notices', 'fetch_tasks' );

    add_shortcode( 'fetch_tasks', 'fetch_tasks' );
    add_shortcode( 'fetch_task', 'fetch_task' );
    add_shortcode( 'update_task', 'update_task' );
    add_shortcode( 'create_task', 'create_task' );
    add_shortcode( 'remove_task', 'remove_task' );

    // add_action( 'fetch_tasks', 'fetch_tasks' );
    // add_action( 'fetch_task', 'fetch_task' );
    // add_action( 'update_task', 'update_task' );
    // add_action( 'create_task', 'create_task' );
    // add_action( 'remove_task', 'remove_task' );

?>