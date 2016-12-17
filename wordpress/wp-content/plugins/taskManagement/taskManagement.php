<?php
    /*
    Plugin Name: Task Management
    Description: Plugin that offers full crud with the assessment task management api
    Author: Stafleu Buitendag
    */

    // Method: POST, PUT, GET etc
    // Data: array("param" => "value") ==> index.php?param=value

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
        echo "[{id, name, status}]";
    }

    //return {id, name, description, dueDate, creationDate, status}
    function fetch_task( $id = null ) {

    }

    //return {id, name, description, dueDate, creationDate, status}
    function update_task( $id, $key, $value ) {

    }

    //return {id, name, description, dueDate, creationDate, status}
    // $name, $description, $dueDate, $status
    function create_task( ) {

    }

    function remove_task( $id ) {

    }
?>